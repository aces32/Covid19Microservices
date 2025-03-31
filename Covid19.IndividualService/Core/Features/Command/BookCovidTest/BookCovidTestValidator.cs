using Covid19.IndividualService.Core.Contracts.Persistence;
using FluentValidation;

namespace Covid19.IndividualService.Core.Features.Command.BookCovidTest
{
    public class BookCovidTestValidator : AbstractValidator<BookCovidTestCommand>
    {
        private readonly IBookingAvailabilityRepository _bookingAvailabilityRepository;
        private readonly IIndividualRepository _individualRepository; 

        public BookCovidTestValidator(IBookingAvailabilityRepository bookingAvailabilityRepository,
            IIndividualRepository individualRepository)
        {
            _bookingAvailabilityRepository = bookingAvailabilityRepository;
            _individualRepository = individualRepository;

            RuleFor(p => p.LocationId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThan(0);

            RuleFor(p => p.IndividualEmailAddress)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .EmailAddress();

            RuleFor(p => p.IndividualFirstName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(150);

            RuleFor(p => p.IndividualLastName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(150);

            RuleFor(p => p.IndividualMobileNumber)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(30);

            RuleFor(p => p.BookingDate)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThan(DateTimeOffset.Now.AddDays(-1));

            RuleFor(e => e)
            .MustAsync(DoesAllocatedBookingExist)
            .WithMessage("Allocated Booking does not exist.");

            RuleFor(e => e)
            .MustAsync(DoesIndividualExist)
            .WithMessage("Individual already booked Covid Test.");

            RuleFor(x => x.IndividualLab.TestType)
                .IsInEnum()
                .WithMessage("Test Type must be either PCRTest or RapidTest.");

        }


        private async Task<bool> DoesIndividualExist(BookCovidTestCommand e, CancellationToken token)
        {
            return !await _individualRepository.AnyAsync(x => x.EmailAddress == e.IndividualEmailAddress 
                                                        && x.MobileNumber == e.IndividualMobileNumber);
        }


        private async Task<bool> DoesAllocatedBookingExist(BookCovidTestCommand e, CancellationToken token)
        {
            // Date is not supported in EF Core for translation to SQL
            var start = e.BookingDate.Date;
            var end = start.AddDays(1);
            return await _bookingAvailabilityRepository.AnyAsync(x =>
                                                                x.LocationId == e.LocationId &&
                                                                x.BookingDate >= start &&
                                                                x.BookingDate < end);
        }
    }
}

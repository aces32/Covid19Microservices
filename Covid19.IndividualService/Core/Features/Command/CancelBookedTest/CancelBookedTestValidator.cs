using Covid19.IndividualService.Core.Contracts.Persistence;
using FluentValidation;

namespace Covid19.IndividualService.Core.Features.Command.CancelBookedTest
{
    public class CancelBookedTestValidator : AbstractValidator<CancelBookedTestCommand>
    {
        private readonly IIndividualRepository _individualRepository;

        public CancelBookedTestValidator(IIndividualRepository individualRepository)
        {
            _individualRepository = individualRepository;

            RuleFor(p => p.IndividualEmailAddress)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .EmailAddress();

            RuleFor(p => p.IndividualMobileNumber)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(30);

            RuleFor(p => p.IndividualBookingStatus)
            .NotNull();

            RuleFor(e => e)
            .MustAsync(DoesIndividualExist)
            .WithMessage("Canceled or no booked Covid tests based on the specified email address and phone number.");
        }

        private async Task<bool> DoesIndividualExist(CancelBookedTestCommand e, CancellationToken token)
        {
            var individual = await _individualRepository.FindWhere(x => x.EmailAddress == e.IndividualEmailAddress
                                                                   && x.MobileNumber == e.IndividualMobileNumber);
            return await _individualRepository.AnyAsync(x => x.EmailAddress == e.IndividualEmailAddress
                                                                   && x.MobileNumber == e.IndividualMobileNumber);
        }
    }
}

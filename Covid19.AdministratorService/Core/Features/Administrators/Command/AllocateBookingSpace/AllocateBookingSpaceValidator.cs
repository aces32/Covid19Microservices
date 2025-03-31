using Covid19.AdministratorService.Core.Contracts.Persistence;
using Covid19.AdministratorService.Core.Features.LocationsHandler.Queries.GetAllAvailableLocationIdsHandler.cs;
using FluentValidation;
using MediatR;

namespace Covid19.AdministratorService.Core.Features.Administrators.Command.AllocateBookingSpace
{
    public class AllocateBookingSpaceValidator : AbstractValidator<AllocateBookingSpaceCommand>
    {
        private readonly IAdminBookingRepository _adminBookingRepository;
        private readonly IMediator _mediator;

        public AllocateBookingSpaceValidator(IAdminBookingRepository adminBookingRepository,
            IMediator mediator)
        {
            _adminBookingRepository = adminBookingRepository;
            _mediator = mediator;
            RuleFor(p => p.Capacity)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThan(0);

            RuleFor(p => p.LocationId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThan(0);

            RuleFor(p => p.BookingDate)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .GreaterThan(DateTimeOffset.Now.AddDays(-1));

            RuleFor(e => e)
            .MustAsync(DoesLocationExistAsync)
            .WithMessage("Location ID does not exist.");

            RuleFor(e => e)
            .MustAsync(DoesAllocatedBookingExist)
            .WithMessage("Booking has been allocated for the specifed location at the specified date.");

        }

        private async Task<bool> DoesLocationExistAsync(AllocateBookingSpaceCommand e, CancellationToken cancellationToken)
        {
            var locationIds = await _mediator.Send(new GetAllAvailableLocationIdsQuery(), cancellationToken);
            return locationIds.Contains(e.LocationId);
        }


        private async Task<bool> DoesAllocatedBookingExist(AllocateBookingSpaceCommand e, CancellationToken token)
        {
            // Date is not supported in EF Core for translation to SQL
            var start = e.BookingDate.Date;
            var end = start.AddDays(1);
            return !await _adminBookingRepository.AnyAsync(x =>
                                                                x.LocationId == e.LocationId &&
                                                                x.BookingDate >= start &&
                                                                x.BookingDate < end);
        }
    }
}

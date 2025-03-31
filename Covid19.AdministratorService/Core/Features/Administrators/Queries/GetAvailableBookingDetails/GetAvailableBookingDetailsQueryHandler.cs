using Covid19.AdministratorService.Core.Contracts.Persistence;
using Covid19.AdministratorService.Core.Features.LocationsHandler.Queries.GetLocationsByIdsHandler;
using MediatR;

namespace Covid19.AdministratorService.Core.Features.Administrators.Queries.GetAvailableBookingDetails
{
    public class GetAvailableBookingDetailsQueryHandler : IRequestHandler<GetAvailableBookingDetailsQuery, List<GetAvailableBookingDetailsQueryResponse>>
    {
        private readonly IAdminBookingRepository _adminBookingRepository;
        private readonly ILogger<GetAvailableBookingDetailsQueryHandler> _logger;
        private readonly IMediator _mediator;

        public GetAvailableBookingDetailsQueryHandler(IAdminBookingRepository adminBookingRepository,
            ILogger<GetAvailableBookingDetailsQueryHandler> logger, IMediator mediator)
        {
            _adminBookingRepository = adminBookingRepository;
            _logger = logger;
            _mediator = mediator;
        }
        public async Task<List<GetAvailableBookingDetailsQueryResponse>> Handle(GetAvailableBookingDetailsQuery request, CancellationToken cancellationToken)
        {
            var allBookings = await _adminBookingRepository.ListAllAsync();
            var locationIds = allBookings.Select(x => x.LocationId).Distinct();
            var locations = await _mediator.Send(new GetLocationsByIdsQuery{ LocationIds = locationIds}, cancellationToken);

            return [.. allBookings.Select(b => new GetAvailableBookingDetailsQueryResponse
            {
                BookingDate = b.BookingDate,
                Capacity = b.Capacity,
                SpaceAllocated = b.SpaceAllocated,
                Location = new LocationDetailDto { LocationName = 
                locations.FirstOrDefault(l => l.LocationID == b.LocationId)?.LocationName 
                ?? throw new ArgumentNullException(nameof(locations), $"No location found with ID {b.LocationId}") },
                Success = true,
                Message = "Booking details retrieved successfully",
            })];
        }
    }
}

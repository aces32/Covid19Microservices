using Covid19.AdministratorService.Core.Contracts.Persistence;
using Covid19.GrpcContracts;
using Grpc.Core;

namespace Covid19.AdministratorService.Infrastructure.SyncDataService
{
    public class BookingAvailabilityGrpcService : BookingAvailabilityService.BookingAvailabilityServiceBase
    {
        private readonly IAdminBookingRepository _adminBookingRepository;

        public BookingAvailabilityGrpcService(IAdminBookingRepository adminBookingRepository)
        {
            _adminBookingRepository = adminBookingRepository;
        }

        public override async Task<GetAllBookingAllocationsResponse> GetAllBookingAllocations(
            GetAllBookingAllocationsRequest request,
            ServerCallContext context)
        {
            var allocations = await _adminBookingRepository.ListAllAsync();

            var response = new GetAllBookingAllocationsResponse();
            response.Bookings.AddRange(allocations.Select(a => new BookingAvailabilityDto
            {
                AdminBookingAllocationId = a.AdminBookingAllocationId,
                LocationId = a.LocationId,
                BookingDate = a.BookingDate.ToString("o"),
                Capacity = a.Capacity,
                SpaceAllocated = a.SpaceAllocated
            }));

            return response;
        }
    }

}

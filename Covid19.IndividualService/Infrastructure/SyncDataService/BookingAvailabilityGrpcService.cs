using AutoMapper;
using Covid19.GrpcContracts;
using Covid19.IndividualService.Core.Contracts.Infrastructure;
using Covid19.IndividualService.Domain.Entities;

namespace Covid19.IndividualService.Infrastructure.SyncDataService
{
    public class BookingAvailabilityGrpcService : IBookingAvailabilitySyncService
    {
        private readonly BookingAvailabilityService.BookingAvailabilityServiceClient _grpcClient;
        private readonly IMapper _mapper;

        public BookingAvailabilityGrpcService(
            BookingAvailabilityService.BookingAvailabilityServiceClient grpcClient,
            IMapper mapper)
        {
            _grpcClient = grpcClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingAvailability>> SyncBookingAllocationAsync()
        {
            var bookingAllocations = await _grpcClient.GetAllBookingAllocationsAsync(new GetAllBookingAllocationsRequest());
            return _mapper.Map<IEnumerable<BookingAvailability>>(bookingAllocations.Bookings);

        }
    }


}

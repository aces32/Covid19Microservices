using Covid19.IndividualService.Core.Contracts.Persistence;
using Covid19.IndividualService.Domain.Entities;
using Covid19.Shared.EventContracts;
using MassTransit;

namespace Covid19.IndividualService.Infrastructure.AsyncDataService
{
    public class BookingSlotCreatedEventConsumer : IConsumer<BookingSlotCreatedEvent>
    {
        private readonly IBookingAvailabilityRepository _bookingAvailabilityRepository;

        public BookingSlotCreatedEventConsumer(IBookingAvailabilityRepository bookingAvailabilityRepository)
        {
            _bookingAvailabilityRepository = bookingAvailabilityRepository;
        }

        public async Task Consume(ConsumeContext<BookingSlotCreatedEvent> context)
        {
            var evt = context.Message;

            // Save or update local projection for availability
            var availability = new BookingAvailability
            {
                AdminBookingAllocationId = evt.AdminBookingAllocationId,
                LocationId = evt.LocationId,
                BookingDate = evt.BookingDate.Date,
                Capacity = evt.Capacity,
                SpaceAllocated = 0 // SpaceAllocated starts at 0
            };

            await _bookingAvailabilityRepository.AddAsync(availability);

        }
    }

}

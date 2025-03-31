namespace Covid19.Shared.EventContracts
{
    public class BookingSlotCreatedEvent
    {
        public int AdminBookingAllocationId { get; set; }
        public int LocationId { get; set; }
        public DateTimeOffset BookingDate { get; set; }
        public int Capacity { get; set; }
    }

}

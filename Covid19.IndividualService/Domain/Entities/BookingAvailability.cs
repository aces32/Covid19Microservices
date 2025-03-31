namespace Covid19.IndividualService.Domain.Entities
{
    public class BookingAvailability
    {
        public int Id { get; set; } 

        public int AdminBookingAllocationId { get; set; }

        public int LocationId { get; set; }

        public DateTime BookingDate { get; set; }

        public int Capacity { get; set; }

        public int SpaceAllocated { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

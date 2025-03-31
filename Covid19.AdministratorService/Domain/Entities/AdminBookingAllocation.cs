namespace Covid19.AdministratorService.Domain.Entities
{
    public class AdminBookingAllocation
    {
        public int AdminBookingAllocationId { get; set; }
        public int Capacity { get; set; }
        public int SpaceAllocated { get; set; }
        public DateTimeOffset BookingDate { get; set; }
        public int LocationId { get; set; }
    }
}

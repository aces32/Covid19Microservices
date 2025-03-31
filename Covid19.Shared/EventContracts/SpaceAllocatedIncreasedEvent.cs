namespace Covid19.Shared.EventContracts
{
    public class SpaceAllocatedIncreasedEvent
    {
        public int AdminBookingAllocationId { get; set; }
        public int SpaceAllocated { get; set; }
    }

}

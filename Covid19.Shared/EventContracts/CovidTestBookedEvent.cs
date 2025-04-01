namespace Covid19.Shared.EventContracts
{
    public class CovidTestBookedEvent
    {
        public int IndividualId { get; set; }
        public required string EmailAddress { get; set; }
        public DateTimeOffset BookingDate { get; set; }
        public int LocationId { get; set; }
        public TestTypeEvent TestType { get; set; }
    }
}

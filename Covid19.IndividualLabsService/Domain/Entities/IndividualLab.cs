namespace Covid19.IndividualLabsService.Domain.Entities
{
    public class IndividualLab
    {
        public int IndividualLabId { get; set; }
        public required TestType TestType { get; set; }
        public required string TestOutCome { get; set; }
        public bool TestCompleted { get; set; }
        public int IndividualId { get; set; }

        public required DateTimeOffset BookingDate { get; set; }
    }
}

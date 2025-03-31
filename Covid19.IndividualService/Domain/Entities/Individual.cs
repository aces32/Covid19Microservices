namespace Covid19.IndividualService.Domain.Entities
{
    public class Individual
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string EmailAddress { get; set; }
        public required string MobileNumber { get; set; }
        public required bool BookingStatus { get; set; } = true;

        public int AdminBookingAllocationId { get; set; }
    }
}

namespace Covid19.IndividualService.Core.Features.Command.BookCovidTest
{
    public record IndividualLabsRequest
    {
        /// <summary>
        /// Type of test individual wants to take
        /// Either pass PCRTest or RapidTest
        /// </summary>
        public required TestType TestType { get; set; }
    }
}

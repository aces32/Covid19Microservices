using Covid19.IndividualLabsService.Models.Response;

namespace Covid19.IndividualLabsService.Core.Features.Command.SetTestOutcome
{
    public record SetTestOutcomeCommandResponse : BaseResponse
    {
        public SetTestOutcomeCommandResponse() : base()
        {

        }
        /// <summary>
        /// Test Outcome if it is positive or negative
        /// Pass Negative or Positive
        /// </summary>
        public required string TestOutCome { get; set; }
        /// <summary>
        /// Indicates that tests has been completed
        /// </summary>
        public bool TestCompleted { get; set; }
    }
}

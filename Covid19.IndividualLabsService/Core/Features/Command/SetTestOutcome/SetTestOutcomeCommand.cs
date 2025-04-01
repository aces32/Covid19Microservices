using MediatR;

namespace Covid19.IndividualLabsService.Core.Features.Command.SetTestOutcome
{
    public record SetTestOutcomeCommand : IRequest<SetTestOutcomeCommandResponse>
    {
        /// <summary>
        /// Individual booking Labs Id
        /// </summary>
        public int IndividualId { get; set; }
        /// <summary>
        /// Outcome of the covid test taken set by the lab administrator
        /// either negative or positive 
        /// </summary>
        public required string TestOutCome { get; set; }
        /// <summary>
        /// Set that tests has been completed
        /// </summary>
        public bool TestCompleted { get; set; }
    }
}

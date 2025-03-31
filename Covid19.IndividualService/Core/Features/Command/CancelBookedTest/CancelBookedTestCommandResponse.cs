using Covid19.IndividualService.Models.Response;

namespace Covid19.IndividualService.Core.Features.Command.CancelBookedTest
{
    public record CancelBookedTestCommandResponse : BaseResponse
    {
        /// <summary>
        /// Individual booking Email Address
        /// </summary>
        public required string IndividualEmailAddress { get; set; }
        /// <summary>
        /// Individual booking Mobile Number
        /// </summary>
        public required string IndividualMobileNumber { get; set; }
    }
}

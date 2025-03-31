using MediatR;

namespace Covid19.IndividualService.Core.Features.Command.CancelBookedTest
{
    public record CancelBookedTestCommand : IRequest<CancelBookedTestCommandResponse>
    {
        /// <summary>
        /// Individual booking Email Address
        /// </summary>
        public required string IndividualEmailAddress { get; set; }
        /// <summary>
        /// Individual booking Mobile Number
        /// </summary>
        public required string IndividualMobileNumber { get; set; }
        /// <summary>
        /// Use to cancel the booking status
        /// </summary>
        public required bool IndividualBookingStatus { get; set; }
    }
}

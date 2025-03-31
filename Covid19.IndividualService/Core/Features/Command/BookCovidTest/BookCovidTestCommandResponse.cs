using Covid19.IndividualService.Models.Response;

namespace Covid19.IndividualService.Core.Features.Command.BookCovidTest
{
    public record BookCovidTestCommandResponse : BaseResponse
    {
        public BookCovidTestCommandResponse() : base()
        {

        }

        /// <summary>
        /// Individual booking first name
        /// </summary>
        public required string IndividualFirstName { get; set; }
        /// <summary>
        /// Individual booking Last name
        /// </summary>
        public required string IndividualLastName { get; set; }
        /// <summary>
        /// Individual booking Email Address
        /// </summary>
        public required string IndividualEmailAddress { get; set; }
        /// <summary>
        /// Individual booking Mobile Number
        /// </summary>
        public required string IndividualMobileNumber { get; set; }
        /// <summary>
        /// Date of booking
        /// </summary>
        public required DateTime BookingDate { get; set; }
    }
}

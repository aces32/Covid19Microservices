using MediatR;
using System;

namespace Covid19.IndividualService.Core.Features.Command.BookCovidTest
{
    public record BookCovidTestCommand : IRequest<BookCovidTestCommandResponse>
    {
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
        public required DateTimeOffset BookingDate { get; set; }
        /// <summary>
        /// Id of the location been booked
        /// </summary>
        public required int LocationId { get; set; }
        /// <summary>
        /// Individual lab details
        /// </summary>
        public required IndividualLabsRequest IndividualLab { get; set; }

    }
}

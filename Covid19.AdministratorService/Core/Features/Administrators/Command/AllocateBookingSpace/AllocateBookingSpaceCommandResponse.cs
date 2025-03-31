using Covid19.AdministratorService.Models.Response;

namespace Covid19.AdministratorService.Core.Features.Administrators.Command.AllocateBookingSpace
{
    public record AllocateBookingSpaceCommandResponse : BaseResponse
    {
        public AllocateBookingSpaceCommandResponse() : base()
        {

        }
        /// <summary>
        /// Total space capacity for a specified Location
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// Available date of booking
        /// </summary>
        public DateTimeOffset BookingDate { get; set; }

    }
}

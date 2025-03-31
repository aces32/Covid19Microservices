using MediatR;

namespace Covid19.AdministratorService.Core.Features.Administrators.Command.AllocateBookingSpace
{
    public class AllocateBookingSpaceCommand : IRequest<AllocateBookingSpaceCommandResponse>
    {
        /// <summary>
        /// Total space capacity for a specified Location
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// Available date of booking
        /// </summary>
        public DateTimeOffset BookingDate { get; set; }
        /// <summary>
        /// Location ID based on the location name selected by the administrator
        /// </summary>
        public int LocationId { get; set; }
    }
}

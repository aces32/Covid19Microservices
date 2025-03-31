using Covid19.AdministratorService.Models.Response;

namespace Covid19.AdministratorService.Core.Features.Administrators.Queries.GetAvailableBookingDetails
{
    public record GetAvailableBookingDetailsQueryResponse : BaseResponse
    {
        public GetAvailableBookingDetailsQueryResponse() : base()
        {
        }

        /// <summary>
        /// Total space capacity for a specified Location
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Total space booked by individuals
        /// </summary>
        public int SpaceAllocated { get; set; }

        /// <summary>
        /// Available date of booking
        /// </summary>
        public DateTimeOffset BookingDate { get; set; }

        /// <summary>
        /// Location of booking
        /// </summary>
        public required LocationDetailDto Location { get; set; }
    }
}

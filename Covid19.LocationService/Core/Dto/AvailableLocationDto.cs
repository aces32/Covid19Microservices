namespace Covid19.LocationService.Core.Dto
{
    public record AvailableLocationDto
    {
        /// <summary>
        /// Location Id to be used by the administrator
        /// </summary>
        public int LocationID { get; set; }
        /// <summary>
        /// Available locations for Covid Tests
        /// </summary>
        public required string LocationName { get; set; }
    }
}

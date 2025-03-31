namespace Covid19.AdministratorService.Models.LocationModels
{
    public record AvailableLocationListDto
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

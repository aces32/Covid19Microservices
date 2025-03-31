namespace Covid19.IndividualService.Models.ConfigurationModels
{
    public class SerilogOptions
    {
        public const string SectionName = "Serilog";

        public string LogIndexName { get; set; } = string.Empty;
    }
}

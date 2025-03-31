namespace Covid19.AdministratorService.Models.ConfigurationModels
{
    public class SerilogOptions
    {
        public const string SectionName = "Serilog";

        public string LogIndexName { get; set; } = string.Empty;
    }
}

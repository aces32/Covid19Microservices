using Covid19.IndividualService.Models.ConfigurationModels;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace Covid19.LocationService.Infrastructure.Logging
{
    public static class Logging
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
            (hostBuilderContext, loggerConfiguration) =>
            {
                IHostEnvironment? env = hostBuilderContext.HostingEnvironment;
                var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
                var options = new SerilogOptions();
                hostBuilderContext.Configuration.GetSection(SerilogOptions.SectionName).Bind(options);
                var logIndexName = string.IsNullOrEmpty(options.LogIndexName) ? env.ApplicationName : options.LogIndexName;
                _ = loggerConfiguration.MinimumLevel.Information()
                .Enrich.FromLogContext()
                .Enrich.WithProperty(nameof(env.ApplicationName), env.ApplicationName)
                .Enrich.WithProperty(nameof(env.EnvironmentName), env.EnvironmentName)
                .Enrich.WithExceptionDetails()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System.Net.Http", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.LifeTime", LogEventLevel.Information)
                .WriteTo.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Traces)
                .WriteTo.Console();

                if (hostBuilderContext.HostingEnvironment.IsDevelopment())
                {
                    _ = loggerConfiguration.MinimumLevel.Override(env.ApplicationName, LogEventLevel.Debug);
                }
            };
    }
}

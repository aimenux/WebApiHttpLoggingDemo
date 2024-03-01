using Api.TelemetryInitializers;
using Api.TelemetryProcessors;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using Serilog.Debugging;
using static Api.Constants;

namespace Api;

public partial class Startup
{
    private static void AddLogging(WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            SelfLog.Enable(Console.Error);

            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext();
        });

        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.RequestHeaders.Add(ApiKeyHeaderName);
            logging.CombineLogs = true;
        });
    }

    private static void UseHttpLogging(WebApplication app)
    {
        app.UseWhen(IsLoggingEnabled, builder =>
        {
            builder.UseHttpLogging();
        });

        static bool IsLoggingEnabled(HttpContext context)
        {
            var path = context.Request.Path.ToString();
            if (path.IgnoreCaseContains("health")) return false;
            if (path.IgnoreCaseContains("swagger")) return false;
            return context.Request.Method == "POST";
        }
    }

    private static void AddApplicationInsightsTelemetry(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetValue<string>("Serilog:WriteTo:2:Args:connectionString");
        var appInsightsOptions = new ApplicationInsightsServiceOptions
        {
            ConnectionString = connectionString,
            DeveloperMode = builder.Environment.IsDevelopment(),
            EnableAppServicesHeartbeatTelemetryModule = false,
            EnableHeartbeat = false
        };
        builder.Services.AddApplicationInsightsTelemetry(appInsightsOptions);
        builder.Services.AddSingleton<ITelemetryInitializer, ApiKeyTelemetryInitializer>();
        builder.Services.AddApplicationInsightsTelemetryProcessor<SwaggerTelemetryProcessor>();
        builder.Services.AddApplicationInsightsTelemetryProcessor<HealthChecksTelemetryProcessor>();
        builder.Services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, _) =>
        {
            module.EnableSqlCommandTextInstrumentation = true;
        });
    }
}
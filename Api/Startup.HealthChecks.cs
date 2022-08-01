using Api.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Api;

public partial class Startup
{
    private static void AddHealthChecks(WebApplicationBuilder builder)
    {
        builder.Services
            .AddHealthChecks()
            .AddPingCheck()
            .AddDebugSessionCheck()
            .AddDatabaseHealthCheck();

        builder.Services
            .AddHealthChecksUI()
            .AddInMemoryStorage();
    }

    private static void MapHealthChecks(WebApplication app)
    {
        app.MapHealthChecks(@"/api/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapHealthChecksUI();
    }
}
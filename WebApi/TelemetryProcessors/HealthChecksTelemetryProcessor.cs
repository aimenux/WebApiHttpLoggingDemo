using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Api.TelemetryProcessors;

public class HealthChecksTelemetryProcessor : AbstractTelemetryProcessor
{
    private readonly ITelemetryProcessor _next;

    public HealthChecksTelemetryProcessor(ITelemetryProcessor next)
    {
        _next = next;
    }

    public override void Process(ITelemetry item)
    {
        if (!IsHealthChecks(item))
        {
            _next.Process(item);
        }
    }

    private static bool IsHealthChecks(ITelemetry item)
    {
        var operationName = GetOperationName(item);
        return operationName.IgnoreCaseContains("health");
    }
}
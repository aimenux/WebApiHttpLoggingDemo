using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Api.TelemetryProcessors;

public class SwaggerTelemetryProcessor : AbstractTelemetryProcessor
{
    private readonly ITelemetryProcessor _next;

    public SwaggerTelemetryProcessor(ITelemetryProcessor next)
    {
        _next = next;
    }

    public override void Process(ITelemetry item)
    {
        if (!IsSwagger(item))
        {
            _next.Process(item);
        }
    }

    private static bool IsSwagger(ITelemetry item)
    {
        var operationName = GetOperationName(item);
        return operationName.IgnoreCaseContains("swagger") || operationName.IgnoreCaseContains("browser");
    }
}
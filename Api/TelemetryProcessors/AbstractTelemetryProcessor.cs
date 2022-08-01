using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Api.TelemetryProcessors;

public abstract class AbstractTelemetryProcessor : ITelemetryProcessor
{
    public abstract void Process(ITelemetry item);

    protected static string GetOperationName(ITelemetry item)
    {
        return item switch
        {
            EventTelemetry eventTelemetry => eventTelemetry.Name,
            MetricTelemetry metricTelemetry => metricTelemetry.Name,
            RequestTelemetry requestTelemetry => requestTelemetry.Name,
            DependencyTelemetry dependencyTelemetry => dependencyTelemetry.Name,
            AvailabilityTelemetry availabilityTelemetry => availabilityTelemetry.Name,
            _ => item?.Context?.Operation?.Name
        };
    }
}
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using static Api.Constants;

namespace Api.TelemetryInitializers;

public class ApiKeyTelemetryInitializer : ITelemetryInitializer
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiKeyTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public void Initialize(ITelemetry telemetry)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context is null) return;
        if (telemetry is not RequestTelemetry requestTelemetry) return;
        if (requestTelemetry.Properties.ContainsKey(ApiKeyHeaderName)) return;
        if (context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValue))
        {
            requestTelemetry.Properties.TryAdd(ApiKeyHeaderName, apiKeyHeaderValue);
        }
    }
}
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Api.HealthChecks;

public class PingHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}
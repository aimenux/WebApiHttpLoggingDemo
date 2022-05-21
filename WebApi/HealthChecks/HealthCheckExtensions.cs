using System.Security.Cryptography;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Api.HealthChecks;

public static class HealthCheckExtensions
{
    public const string PingTag = "ping";
    public const string DatabaseTag = "database";
    public const string DebugSessionTag = "debug";

    public static readonly TimeSpan Timeout = TimeSpan.FromSeconds(5);

    public static IHealthChecksBuilder AddPingCheck(this IHealthChecksBuilder builder)
    {
        return builder.AddCheck<PingHealthCheck>(nameof(PingHealthCheck), tags: new List<string> { PingTag }, timeout: Timeout);
    }

    public static IHealthChecksBuilder AddDatabaseHealthCheck(this IHealthChecksBuilder builder)
    {
        return builder.AddCheck<DatabaseHealthCheck>(nameof(DatabaseHealthCheck), tags: new List<string> { DatabaseTag }, timeout: Timeout);
    }

    public static IHealthChecksBuilder AddDebugSessionCheck(this IHealthChecksBuilder builder)
    {
#if DEBUG
        return builder.AddCheck<DebugHealthCheck>(nameof(DebugHealthCheck), tags: new List<string> { DebugSessionTag }, timeout: Timeout);
#else
            return builder;
#endif
    }

    public class DebugHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var next = RandomNumberGenerator.GetInt32(100, 500);

            await Task.Delay(next, cancellationToken);

            var result = next switch
            {
                var n when (n % 2 == 0) => HealthCheckResult.Healthy(),
                var n when (n % 5 == 0) => HealthCheckResult.Degraded(),
                _ => HealthCheckResult.Unhealthy()
            };

            return result;
        }
    }
}
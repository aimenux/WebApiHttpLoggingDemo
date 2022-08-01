using Domain.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Api.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly QuoteDbContext _context;

    public DatabaseHealthCheck(QuoteDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var result = await _context.Database.CanConnectAsync(cancellationToken);
        return result ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
    }
}
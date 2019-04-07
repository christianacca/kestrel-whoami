using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace App.Health
{
    public class ToggledHealthCheck : IHealthCheck
    {
        public ToggledHealthCheck(HealthCheckToggle toggle)
        {
            Toggle = toggle;
        }

        private HealthCheckToggle Toggle { get; }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Toggle.Healthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }

            return Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}
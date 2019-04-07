using System.Net;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using App.Shared;

namespace App.Health
{
    public class HealthCheckOptionsSetup: IConfigureOptions<HealthCheckOptions>
    {
        public void Configure(HealthCheckOptions options)
        {
            options.ResponseWriter = async (context, healthReport) =>
            {
                await context.WriteModelAsync(new
                {
                    Status = healthReport.Status.ToString(),
                    Host = Dns.GetHostName()
                });
            };
        }
    }
}
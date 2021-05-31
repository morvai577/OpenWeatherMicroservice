using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace MicroserviceApi
{
    public class ExternalEndpointHealthCheck : IHealthCheck
    {
        private readonly ServiceSettings _settings;

        public ExternalEndpointHealthCheck(IOptions<ServiceSettings> options)
        {
            _settings = options.Value;
        }

        /// <summary>
        /// This method pings the microservice to check its health and whether it is available
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<HealthCheckResult></returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping = new();
            var reply = await ping.SendPingAsync(_settings.OpenWeatherHost);

            if (reply.Status != IPStatus.Success)
            {
                return HealthCheckResult.Unhealthy();
            }

            return HealthCheckResult.Healthy();
        }
    }
}
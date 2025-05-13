using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheckTools
{
    public class WebhookHealthCheckPublisher : IHealthCheckPublisher
    {
        private readonly WebhookService _webhookService;

        public WebhookHealthCheckPublisher(WebhookService webhookService)
        {
            _webhookService = webhookService;
        }

        public async Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            if (report.Status != HealthStatus.Healthy)
            {
                var message = $"Health Check failed! Status: {report.Status}, Duration: {report.TotalDuration}, " +
                              $"Details: {string.Join(", ", report.Entries.Select(e => $"{e.Key}: {e.Value.Status}"))}";
                await _webhookService.SendNotificationAsync(message);
            }
        }
    }
}

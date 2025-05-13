using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheckTools
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("Connection not found");

            var baseUrl = configuration["Application:BaseUrl"]
                ?? throw new InvalidOperationException("Invalid BaseUrl.");

            services.AddHttpClient<WebhookService>();
            services.AddSingleton<IHealthCheckPublisher, WebhookHealthCheckPublisher>();

            services.AddHealthChecks()
                .AddMySql(connectionString, name: "MySQL Database", tags: new[] { "Databases Connection" });

            services.AddHealthChecks()
                .AddCheck("API Health", () => HealthCheckResult.Healthy("API is up and running"));

            services.AddHealthChecks()
                .AddCheck("Server Health", () => HealthCheckResult.Degraded("Server is under high load"));

            services.AddHealthChecks()
                .AddUrlGroup(new Uri($"{baseUrl}/api/Category"), name: "Health check for errors");

            services.AddHealthChecks()
                .AddCheck("Configuration Check", () => {
                    if (string.IsNullOrEmpty(configuration["MyConfigKey"]))
                    {
                        return HealthCheckResult.Unhealthy("Configuration missing or invalid.");
                    }
                    return HealthCheckResult.Healthy("Configuration is valid.");
                });
            

            services.AddHealthChecks()
                .AddProcessAllocatedMemoryHealthCheck(512 * 1024 * 1024, "Memory Check", tags: new[] { "System Resources" })
                .AddPrivateMemoryHealthCheck(1024 * 1024 * 1024, "Private Memory Check", tags: new[] { "System Resources" });

            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.SetEvaluationTimeInSeconds(10); // step 10s
                setup.MaximumHistoryEntriesPerEndpoint(60);
                setup.SetApiMaxActiveRequests(1);
            }).AddInMemoryStorage();


            return services;
        }
    }
}

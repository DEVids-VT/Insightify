using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Insightify.Framework.HealthChecks
{

    public static class CoreServicesBuilderExtensions
    {
        /// <summary>
        /// Adds standard Insightify Health Checking functionality
        /// </summary>
        /// <param name="builder">Core Services Builder</param>
        /// <param name="healthCheckBuilder">Health Check Builder</param>
        /// <returns><see cref="IHealthChecksBuilder"/> instance to add specific health checks</returns>
        public static CoreServicesBuilder AddHealthChecks(this CoreServicesBuilder builder, Action<IHealthChecksBuilder> healthCheckBuilder)
        {
            builder.Services.Configure<HealthCheckPublisherOptions>(p =>
            {
                // Sets the initial delay for Health Checks to run (Prevent false negatives during startup)
                p.Delay = TimeSpan.FromSeconds(20);
            });

            healthCheckBuilder.Invoke(builder.Services.AddHealthChecks());

            return builder;
        }
    }
}

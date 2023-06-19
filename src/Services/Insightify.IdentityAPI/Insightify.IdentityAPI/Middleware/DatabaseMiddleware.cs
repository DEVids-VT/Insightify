using Insightify.IdentityAPI.Data;
using Insightify.IdentityAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Diagnostics.CodeAnalysis;

namespace Insightify.IdentityAPI.Middleware
{
    [ExcludeFromCodeCoverage]
    public class DatabaseMiddleware
    {
        public static async Task MigrateDatabase(IServiceScope scope, IConfiguration config, ILogger logger)
        {
            var retryPolicy = CreateRetryPolicy(config, logger);
            var context = scope.ServiceProvider.GetRequiredService<IdentityApiDbContext>();
            await retryPolicy.ExecuteAsync(async () =>
            {
                await context.Database.MigrateAsync();
            });
        }

        private static AsyncPolicy CreateRetryPolicy(IConfiguration configuration, ILogger logger)
        {
            bool.TryParse(configuration["RetryMigrations"], out bool retryMigrations);

            if (retryMigrations)
            {
                return Policy.Handle<Exception>()
                    .CircuitBreakerAsync(
                        exceptionsAllowedBeforeBreaking: 3,
                        durationOfBreak: TimeSpan.FromSeconds(30),
                        onBreak: (ex, breakDuration) =>
                        {
                            logger.LogWarning(
                                ex,
                                "Circuit breaker opened for {breakDuration} due to exception {ExceptionType} with message {Message}",
                                breakDuration,
                                ex.GetType().Name,
                                ex.Message);
                        },
                        onReset: () =>
                        {
                            logger.LogInformation("Circuit breaker reset");
                        },
                        onHalfOpen: () =>
                        {
                            logger.LogInformation("Circuit breaker half-opened");
                        }
                    );
            }

            return Policy.NoOpAsync();
        }
    }
}

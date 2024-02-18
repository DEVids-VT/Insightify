using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Insightify.Posts.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
namespace Insightify.Posts.Infrastructure.Middlewares
{

    [ExcludeFromCodeCoverage]
    public class DatabaseMiddleware
    {
        public static async Task MigrateDatabase(IServiceScope scope, IConfiguration config, ILogger logger)
        {
            var retryPolicy = CreateRetryPolicy(config, logger);
            var context = scope.ServiceProvider.GetRequiredService<PostsDbContext>();
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
                    .RetryAsync(
                        retryCount: 3,
                        onRetry: (exception, retryCount, context) =>
                        {
                            logger.LogWarning(
                                "Retry {retryCount} due to exception {ExceptionType} with message {Message}",
                                retryCount,
                                exception.GetType().Name,
                                exception.Message);
                        });
            }

            return Policy.NoOpAsync();
        }
    }

}

using Microsoft.AspNetCore.Builder;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.FinancialBackgroundTasks.Jobs;
using Quartz;

namespace Insightify.FinancialBackgroundTasks.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, string? connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Invalid Redis connection string.", nameof(connectionString));
            }

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connectionString));
            services.AddTransient<IDatabase>(provider =>
            {
                var multiplexer = provider.GetRequiredService<IConnectionMultiplexer>();
                return multiplexer.GetDatabase();
            });

            return services;
        }
        public static IServiceCollection AddQuartzNET(this IServiceCollection services)
        {
            services.AddQuartz(qz =>
            {
                var jobKey = new JobKey("BitcoinMarketChart");
                qz.AddJob<MarketChartsJob>(opts =>
                {
                    opts.WithIdentity(jobKey);
                });

                qz.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("FetchNewsJob-trigger")
                    .StartNow()
                    .WithSimpleSchedule(s => s.WithIntervalInMinutes(1))
                );
            });
            services.AddQuartzHostedService(qz => qz.WaitForJobsToComplete = true);
            return services;
        }
    }
}

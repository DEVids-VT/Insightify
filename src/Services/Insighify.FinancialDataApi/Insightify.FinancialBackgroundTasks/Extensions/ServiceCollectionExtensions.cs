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
                var marketChartsJobKey = new JobKey("MarketCharts");
                qz.AddJob<MarketChartsJob>(opts =>
                {
                    opts.WithIdentity(marketChartsJobKey);
                });

                qz.AddTrigger(opts => opts
                    .ForJob(marketChartsJobKey)
                    .WithIdentity("FetchCharts-trigger")
                    .StartNow()
                    .WithSimpleSchedule(s => s.WithIntervalInMinutes(1))
                );

                var cryptoCurrencyJobKey = new JobKey("CryptoCurrencies");
                qz.AddJob<CryptoCurrencyJob>(opts =>
                {
                    opts.WithIdentity(cryptoCurrencyJobKey);
                });

                qz.AddTrigger(opts => opts
                    .ForJob(cryptoCurrencyJobKey)
                    .WithIdentity("FetchCurrencies-trigger")
                    .StartNow()
                    .WithSimpleSchedule(s => s.WithIntervalInMinutes(3))
                );
            });
            services.AddQuartzHostedService(qz => qz.WaitForJobsToComplete = true);
            return services;
        }
    }
}

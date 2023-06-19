using Insightify.NewsBackgroundTasks.Jobs;
using Insightify.NewsBackgroundTasks.Profiles;
using Insightify.NewsBackgroundTasks.Services;
using Insightify.NewsBackgroundTasks.Services.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Quartz;
using System.Diagnostics.CodeAnalysis;

namespace Insightify.NewsBackgroundTasks.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomConfiguration(this WebApplicationBuilder builder)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();


            builder.Configuration.AddConfiguration(configBuilder.Build()).Build();
        }

        public static void AddQuartzNET(this WebApplicationBuilder builder)
        {
            builder.Services.AddQuartz(qz =>
            {
                qz.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = new JobKey("FetchNewsJob");
                qz.AddJob<NewsFetcherJob>(opts =>
                {
                    opts.WithIdentity(jobKey);
                });

                qz.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("FetchNewsJob-trigger")
                    .WithCronSchedule("0 0 8 * * ?")
                );
            });
            builder.Services.AddQuartzHostedService(qz => qz.WaitForJobsToComplete = true);
        }
        public static void AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<INewsService, NewsService>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.Fetching.Configuration;
using Insightify.Framework.Fetching.Handlers;
using Insightify.Framework.Fetching.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Insightify.Framework.Fetching.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static CoreServicesBuilder AddApiFetcher(this CoreServicesBuilder builder,
            Action<FetchingConfiguration> configuration)
        {
            builder.Services.AddApiFetcher(configuration);
            return builder;
        }
        public static IServiceCollection AddApiFetcher(this IServiceCollection services,
            Action<FetchingConfiguration> configuration)
        {
            var config = new FetchingConfiguration();
            configuration(config);
            services.Configure(configuration);
            if (string.IsNullOrEmpty(config.BaseUrl))
            {
                throw new ArgumentNullException("", "ApiFetcher Base Url String is empty");
            }
            if (string.IsNullOrEmpty(config.Name))
            {
                throw new ArgumentNullException("", "ApiFetcher Name String is empty");
            }

            var httpBuilder = services.AddHttpClient<IApiFetcher, ApiFetcher>(config.Name, (sp, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(config.BaseUrl);
                httpClient.Timeout = config.Timeout;
                
            });
            if (config.UsesApiKey)
            {
                httpBuilder.AddHttpMessageHandler(() =>
                {
                    var apiKeyHandler = new ApiKeyHandler(config.ApiKey!, config.ApiKeyQueryParemeter!);
                    return apiKeyHandler;
                });
            }

            return services;
        }
    }
}

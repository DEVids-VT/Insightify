using Insightify.Web.Gateway.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using Insightify.Web.Gateway.Clients;
using Insightify.Web.Gateway.Configuration;
using Insightify.Web.Gateway.Services.FinancialData;
using Insightify.Web.Gateway.Services.News;
using Insightify.Web.Gateway.Services.Posts;
using Newtonsoft.Json;
using Refit;

namespace Insightify.Web.Gateway.Extensions
{
    [ExcludeFromCodeCoverage]

    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceEndpoints = configuration
                .GetSection(nameof(ServiceEndpoints))
                .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IFinancialDataService, FinancialDataService>();
            

            services.AddRefitClient<INewsClient>(new RefitSettings()
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer()
                })
                .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(serviceEndpoints.News))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddRefitClient<IPostsClient>(new RefitSettings()
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer()
                })
                .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(serviceEndpoints.Posts))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
            services.AddRefitClient<IFinancialDataClient>(new RefitSettings()
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer()
                })
                .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(serviceEndpoints.FinancialData))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddRefitClient<IProfilesClient>(new RefitSettings()
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            })
                .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(serviceEndpoints.Account))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
            return services;
        }
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication("Bearer").AddJwtBearer(options => {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "gateway";
                options.TokenValidationParameters.ValidateAudience = false;
            });
            

            return services;
        }
    }
}

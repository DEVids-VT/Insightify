using Insightify.Web.Gateway.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using Insightify.Web.Gateway.Clients;
using Insightify.Web.Gateway.Configuration;
using Insightify.Web.Gateway.Services.News;
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

            services.AddRefitClient<INewsClient>()
                .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(serviceEndpoints.News))
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
            services.AddAuthorization(options => {
                options.AddPolicy("ApiScope", policy => {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "gateway");
                });
            });

            return services;
        }
    }
}

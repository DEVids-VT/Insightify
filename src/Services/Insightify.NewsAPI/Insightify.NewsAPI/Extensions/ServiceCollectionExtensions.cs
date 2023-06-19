using Insightify.NewsAPI.Services;
using System.Diagnostics.CodeAnalysis;

namespace Insightify.NewsAPI.Extensions
{
    [ExcludeFromCodeCoverage]

    public static class ServiceCollectionExtensions
    {
        
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INewsReadService, NewsReadService>();
            return services;
        }
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication("Bearer").AddJwtBearer(options => {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "news";
                options.TokenValidationParameters.ValidateAudience = false;
            });
            services.AddAuthorization(options => {
                options.AddPolicy("ApiScope", policy => {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "news");
                });
            });

            return services;
        }
    }
}

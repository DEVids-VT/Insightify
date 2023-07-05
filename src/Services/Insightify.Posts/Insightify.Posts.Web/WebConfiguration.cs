using FluentValidation;
using Insightify.Framework;
using Insightify.Framework.HealthChecks;
using Insightify.Framework.Swagger;
using Insightify.Framework.Swagger.Settings;
using Insightify.Posts.Application.Common;
using Insightify.Posts.Application.Common.Gateways;
using Insightify.Posts.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Insightify.Posts.Web
{
    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(this IServiceCollection services, IConfiguration config)
        {
            var swaggerSettings = config.GetSection("Swagger").Get<SwaggerSettings>();
            var oAuthSettings = config.GetSection("SwaggerSecurity").Get<OAuthSecuritySettings>();


            services.AddCoreServices(coreBuilder =>
            {
                coreBuilder.AddSwagger(p =>
                {
                    p.LoadSettingsFrom(swaggerSettings!);
                    p.LoadSecuritySettingsFrom(oAuthSettings!);
                });
                coreBuilder.AddHealthChecks(p =>
                {
                    p.AddSqlServer(config.GetConnectionString("DefaultConnection")!);
                });
            });
            services.AddValidatorsFromAssemblyContaining<Result>();
            services.AddScoped<ICurrentUser, CurrentUserService>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication("Bearer").AddJwtBearer(options => {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "posts";
                options.TokenValidationParameters.ValidateAudience = false;
            });
            services.AddAuthorization(options => {
                options.AddPolicy("ApiScope", policy => {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "posts");
                });
            });

            return services;
        }
    }
}

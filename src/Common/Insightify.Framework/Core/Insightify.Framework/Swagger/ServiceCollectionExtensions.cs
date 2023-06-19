using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.Swagger.Configuration;
using Insightify.Framework.Swagger.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Insightify.Framework.Swagger
{
    /// <summary>
    /// Extension methods for Service Collection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Swagger with the provided <see cref="SwaggerConfiguration"/>
        /// </summary>
        /// <param name="builder">Core Services Builder</param>
        /// <param name="configuration">Swagger Configuration</param>
        public static CoreServicesBuilder AddSwagger(this CoreServicesBuilder builder, Action<SwaggerConfiguration> configuration)
        {
            var swaggerConfig = new SwaggerConfiguration();
            configuration(swaggerConfig);

            var swaggerSettings = swaggerConfig.Settings;
            var securitySettings = swaggerConfig.Security;

            builder.Services.AddSwaggerFromConfiguration(swaggerSettings, securitySettings);
            return builder;
        }

        /// <summary>
        /// Adds Swagger with the provided <see cref="SwaggerConfiguration"/>
        /// </summary>
        /// <param name="services">Services Collection</param>
        /// <param name="configuration">Swagger Configuration</param>
        public static IServiceCollection AddSwagger(this IServiceCollection services, Action<SwaggerConfiguration> configuration)
        {
            var swaggerConfig = new SwaggerConfiguration();
            configuration(swaggerConfig);

            var swaggerSettings = swaggerConfig.Settings;
            var securitySettings = swaggerConfig.Security;

            services.AddSwaggerFromConfiguration(swaggerSettings, securitySettings);
            return services;
        }

        private static void AddSwaggerFromConfiguration(this IServiceCollection services, SwaggerSettings settings, OAuthSecuritySettings? securitySettings = null)
        {
            settings.ValidateAndThrow();
            var apiVersion = settings.ApiVersionText;

            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc(apiVersion, new OpenApiInfo()
                {
                    Title = settings.ApiName,
                    Version = apiVersion
                });
                if (securitySettings != null)
                {
                    opts.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows()
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri($"{securitySettings.IdentityUrl}/connect/authorize"),
                                TokenUrl = new Uri($"{securitySettings.IdentityUrl}/connect/token"),
                                Scopes = securitySettings.OAuthScopes
                            }
                        }
                    });
                    opts.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Id = "oauth2", Type = ReferenceType.SecurityScheme }
                            },
                            securitySettings.OAuthScopes.Keys.ToArray()
                        }
                    });
                }
                
            });


        }
    }
}

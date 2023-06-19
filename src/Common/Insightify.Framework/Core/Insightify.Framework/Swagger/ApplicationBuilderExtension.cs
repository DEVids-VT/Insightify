using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.Swagger.Settings;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Insightify.Framework.Swagger
{
    /// <summary>
    /// Extension methods for the <see cref="IApplicationBuilder"/>
    /// </summary>
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// Adds Swagger and Swagger UI with Swagger Settings
        /// </summary>
        /// <param name="builder">Application Builder</param>
        /// <param name="swaggerSettings">Swagger Settings</param>
        public static void UseSwagger(this IApplicationBuilder builder, SwaggerSettings swaggerSettings)
        {
            if (swaggerSettings == null)
            {
                throw new ArgumentNullException(nameof(swaggerSettings));
            }

            swaggerSettings.ValidateAndThrow();

            var options = new SwaggerUIOptions
            {
                DocumentTitle = swaggerSettings.ApiName
            };

            if (!string.IsNullOrEmpty(swaggerSettings.RoutePrefix))
            {
                options.RoutePrefix = swaggerSettings.RoutePrefix;
            }

            // Configure Swagger
            builder.UseSwagger(p =>
            {
                p.RouteTemplate = swaggerSettings.JsonRoute;
            });

            options.SwaggerEndpoint(swaggerSettings.UiEndpoint, swaggerSettings.ApiName);
            options.DisplayRequestDuration();
            options.DocExpansion(DocExpansion.None);

            builder.UseSwagger(p => p.RouteTemplate = swaggerSettings.JsonRoute);
            builder.UseSwaggerUI(p =>
            {
                p.SwaggerEndpoint(swaggerSettings.UiEndpoint, swaggerSettings.ApiName);
                p.DocExpansion(DocExpansion.None);
                p.DisplayRequestDuration();
                p.DocumentTitle = swaggerSettings.ApiName;
                p.RoutePrefix = swaggerSettings.RoutePrefix;
            });

            builder.UseMiddleware<SwaggerUIMiddleware>(options);
        }
    }
}

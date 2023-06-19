using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.Swagger.Settings
{
    public class SwaggerSettings
    {
        public string ApiVersionText => ("v" + ApiVersion).Substring(0, 2);
        public string ApiVersion { get; set; } = "1.0.0";
        public string ApiName { get; set; } = "Api";
        public string? JsonRoute { get; set; }
        public string UiEndpoint { get; set; } = "v1/swagger.json";
        public string RoutePrefix { get; set; } = "";

        /// <summary>
        /// Sets the Route Prefix Value
        /// </summary>
        /// <param name="prefix">Route Prefix</param>
        /// <returns><see cref="SwaggerSettings"/></returns>
        public SwaggerSettings WithRoutePrefix(string prefix)
        {
            RoutePrefix = prefix;
            return this;
        }

        /// <summary>
        /// Validates the configuration and throws exceptions if not valid
        /// </summary>
        public void ValidateAndThrow()
        {
            if (string.IsNullOrEmpty(ApiName))
            {
                throw new ArgumentNullException(nameof(ApiName));
            }

            if (string.IsNullOrEmpty(JsonRoute))
            {
                throw new ArgumentNullException(nameof(JsonRoute));
            }

            if (string.IsNullOrEmpty(UiEndpoint))
            {
                throw new ArgumentNullException(nameof(UiEndpoint));
            }
        }
    }
}

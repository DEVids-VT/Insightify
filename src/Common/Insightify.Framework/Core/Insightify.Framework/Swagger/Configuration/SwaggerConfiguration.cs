using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.Swagger.Settings;

namespace Insightify.Framework.Swagger.Configuration
{
    public class SwaggerConfiguration
    {
        internal SwaggerSettings Settings { get; set; } = null!;
        internal OAuthSecuritySettings? Security { get; set; }

        /// <summary>
        /// Loads settings from a <see cref="SwaggerSettings"/> instance
        /// </summary>
        /// <param name="swaggerSettings">Swagger Settings</param>
        public void LoadSettingsFrom(SwaggerSettings swaggerSettings) => this.Settings = swaggerSettings;

        // <summary>
        /// Loads security settings from a SecuritySettings instance
        /// </summary>
        /// <param name="securitySettings">Security Settings</param>
        public void LoadSecuritySettingsFrom(OAuthSecuritySettings securitySettings)
        {
            this.Security = securitySettings;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.Swagger.Settings
{
    public class OAuthSecuritySettings
    {
        public string IdentityUrl { get; set; }
        public Dictionary<string, string> OAuthScopes { get; set; }
    }
}

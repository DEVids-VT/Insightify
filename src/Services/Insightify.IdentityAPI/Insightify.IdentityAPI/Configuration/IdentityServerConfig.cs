using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace Insightify.IdentityAPI.Configuration
{
    public class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>();
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>();
        }

        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientId = "js",
                    ClientName = "Insightify SPA OpenId Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    Enabled = true,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { "http://localhost:4200/callback" },
                    RequireConsent = false,
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    ClientSecrets =
                    {
                        new Secret("angular bog".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                },
            };
        }
    }
}

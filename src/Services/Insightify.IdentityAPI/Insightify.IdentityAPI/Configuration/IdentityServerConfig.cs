using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace Insightify.IdentityAPI.Configuration
{
    public class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>()
            {
                new ApiResource("posts", "Posts Service"),
                new ApiResource("news", "News Service"),
                new ApiResource("gateway", "Web Gateway"),
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("posts", "Posts Service"),
                new ApiScope("news", "News Service"),
                new ApiScope("gateway", "Web Gateway"),
            };
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
                new Client
                {
                    ClientId = "postsswaggerui",
                    ClientName = "Posts Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"http://localhost:5036/oauth2-redirect.html" },
                    //PostLogoutRedirectUris = { $"{configuration["PostsApiClient"]}/swagger/" },
                    AllowedCorsOrigins = { "http://localhost:5036" },
                    AllowedScopes =
                    {
                        "posts"
                    }
                },
                new Client
                {
                    ClientId = "newsswaggerui",
                    ClientName = "News Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"http://localhost:5035/swagger/oauth2-redirect.html" },
                    //PostLogoutRedirectUris = { $"{configuration["PostsApiClient"]}/swagger/" },
                    AllowedCorsOrigins = { "http://localhost:5035" },
                    AllowedScopes =
                    {
                        "news"
                    }
                },
                new Client
                {
                    ClientId = "gatewayswaggerui",
                    ClientName = "Gateway Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"http://localhost:5030/oauth2-redirect.html" },
                    //PostLogoutRedirectUris = { $"{configuration["PostsApiClient"]}/swagger/" },
                    AllowedCorsOrigins = { "http://localhost:5030" },
                    AllowedScopes =
                    {
                        "gateway",
                        "news"
                    }
                },
            };
        }
    }
}

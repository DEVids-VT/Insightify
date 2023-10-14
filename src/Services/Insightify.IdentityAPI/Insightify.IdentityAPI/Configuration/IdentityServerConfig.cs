using Duende.IdentityServer.Models;
using Duende.IdentityServer;
using static System.Net.WebRequestMethods;

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
                //new Client
                //{
                //    ClientId = "js",
                //    ClientName = "Insightify SPA OpenId Client",
                //    AllowedGrantTypes = GrantTypes.Code,
                //    Enabled = true,
                //    AllowAccessTokensViaBrowser = true,
                //    RedirectUris = { $"{configuration["SpaClient"]}/callback" },
                //    RequireConsent = false,
                //    PostLogoutRedirectUris = { $"{configuration["SpaClient"]}" },
                //    AllowedCorsOrigins = { $"{configuration["SpaClient"]}" },
                //    ClientSecrets =
                //    {
                //        new Secret("angular bog".Sha256())
                //    },
                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //    },
                //},
                //new Client
                //{
                //    ClientId = "blazor",
                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequirePkce = true,
                //    RequireClientSecret = false,
                //    AllowedCorsOrigins = { "http://localhost:5099" },
                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //    },                    
                //    RedirectUris = { "http://localhost:5099/authentication/login-callback" },
                //    PostLogoutRedirectUris = { "http://localhost:5099/authentication/logout-callback" },

                //},
                new Client
                {
                    ClientId = "postsswaggerui",
                    ClientName = "Posts Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["PostsApiClient"]}/oauth2-redirect.html" },
                    //PostLogoutRedirectUris = { $"{configuration["PostsApiClient"]}/swagger/" },
                    AllowedCorsOrigins = { $"{configuration["PostsApiClient"]}" },
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

                    RedirectUris = { $"{configuration["NewsApiClient"]}/swagger/oauth2-redirect.html" },
                    //PostLogoutRedirectUris = { $"{configuration["PostsApiClient"]}/swagger/" },
                    AllowedCorsOrigins = { $"{configuration["NewsApiClient"]}" },
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

                    RedirectUris = { $"{configuration["GatewayApiClient"]}/oauth2-redirect.html" },
                    //PostLogoutRedirectUris = { $"{configuration["PostsApiClient"]}/swagger/" },
                    AllowedCorsOrigins = { $"{configuration["GatewayApiClient"]}" },
                    AllowedScopes =
                    {
                        "gateway",
                        "news"
                    }
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    ClientUri = $"{configuration["MVCClient"]}",
                    ClientSecrets = new List<Secret>
                    {

                        new Secret("S0M3 MAG1C UN!C0RNS CR3AT3D TH1S S3CR3T".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = false,
                    RedirectUris = new List<string>
                    {
                        $"{configuration["MVCClient"]}/signin-oidc"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "posts",
                        "news",
                        "gateway",
                    },
                    AccessTokenLifetime = 60*60*2, // 2 hours
                    IdentityTokenLifetime= 60*60*2 // 2 hours
                },
            };
        }
    }
}

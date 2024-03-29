﻿using Insightify.MVC.Clients;
using Insightify.MVC.Infrastructure;
using Insightify.MVC.Services.FinancialData;
using Insightify.MVC.Services.News;
using Insightify.MVC.Services.Posts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Refit;

namespace Insightify.MVC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(setup => setup.ExpireTimeSpan = TimeSpan.FromMinutes(60))
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = identityUrl.ToString();
                
                //options.SignedOutRedirectUri = callBackUrl.ToString();
                options.ClientId = "mvc";
                options.ClientSecret = "S0M3 MAG1C UN!C0RNS CR3AT3D TH1S S3CR3T";
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.RequireHttpsMetadata = false;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("posts");
                options.Scope.Add("news");
                options.Scope.Add("gateway");
                options.ClaimActions.MapJsonKey("profile_picture", "profile_picture");
                options.ClaimActions.MapJsonKey("email", "email");
                options.ClaimActions.MapJsonKey("username", "username");

                options.Events = new OpenIdConnectEvents
                {
                    OnRedirectToIdentityProvider = context =>
                    {
                        // Determine if the request is for the /connect/authorize endpoint
                        if (context.ProtocolMessage.RequestType == OpenIdConnectRequestType.Authentication)
                        {
                            // Replace the internal URL with the external URL
                            var externalIdentityUrl = "http://localhost:5001";
                            context.ProtocolMessage.IssuerAddress = externalIdentityUrl + context.ProtocolMessage.IssuerAddress.Substring(context.Options.Authority.Length);
                        }

                        return Task.CompletedTask;
                    },
                    // Other event handlers if needed...
                };
            });

            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var gatewayUrl = configuration.GetValue<string>("GatewayUrl") ?? "http://localhost";

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IFinancialDataService, FinancialDataService>();

            services.AddRefitClient<IPostsClient>(new RefitSettings()
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer()
                })
                .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(gatewayUrl))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
            services.AddRefitClient<INewsClient>(new RefitSettings()
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer()
                })
                .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(gatewayUrl))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
            services.AddRefitClient<IFinancialDataClient>(new RefitSettings()
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer()
                })
                .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(gatewayUrl))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddRefitClient<IProfilesClient>(new RefitSettings()
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            })
                .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(gatewayUrl))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();


            return services;
        }
    }
}

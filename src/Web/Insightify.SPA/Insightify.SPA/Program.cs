using Insightify.SPA;
using Insightify.SPA.Clients;
using Insightify.SPA.Configuration;
using Insightify.SPA.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var gatewayEndpoints = builder.Configuration
    .GetSection(nameof(GatewayEndpoints))
    .Get<GatewayEndpoints>(config => config.BindNonPublicProperties = true);

builder.Services.AddTransient<AuthorizationMessageHandler>();

builder.Services.AddRefitClient<INewsClient>()
    .ConfigureHttpClient(cfg => cfg.BaseAddress = new Uri(gatewayEndpoints.Web))
    .AddHttpMessageHandler<AuthorizationMessageHandler>();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("oidc", options.ProviderOptions);
});

await builder.Build().RunAsync();
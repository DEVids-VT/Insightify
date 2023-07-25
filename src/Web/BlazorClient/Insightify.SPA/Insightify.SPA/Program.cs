using Insightify.SPA;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    // Configuring the options directly, replace the following values with your actual settings
    options.ProviderOptions.Authority = "http://localhost:5001";
    options.ProviderOptions.ClientId = "blazor";
    // If you have other scopes, add them here as well
    options.ProviderOptions.RedirectUri = "http://localhost:5099/authentication/login-callback";
    options.ProviderOptions.PostLogoutRedirectUri = "http://localhost:5099/authentication/logout-callback";
    options.ProviderOptions.ResponseType = "code";
});

await builder.Build().RunAsync();

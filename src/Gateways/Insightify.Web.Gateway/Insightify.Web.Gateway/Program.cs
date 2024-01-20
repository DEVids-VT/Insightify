using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using Azure.Core;
using FluentValidation;
using Insightify.Framework;
using Insightify.Framework.Automapper;
using Insightify.Framework.HealthChecks;
using Insightify.Framework.Logging;
using Insightify.Framework.Pagination.Headers;
using Insightify.Framework.Swagger;
using Insightify.Framework.Swagger.Settings;
using Insightify.Web.Gateway.Extensions;
using Insightify.Web.Gateway.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory()
});
IdentityModelEventSource.ShowPII = true;

builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Any, 5030,
        listenOptions => { listenOptions.Protocols = HttpProtocols.Http1AndHttp2; });
});
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddEnvironmentVariables();
var swaggerSettings = builder.Configuration.GetSection("Swagger").Get<SwaggerSettings>();

builder.Services.AddCoreServices(coreBuilder =>
{
    var oAuthSettings = builder.Configuration.GetSection("SwaggerSecurity").Get<OAuthSecuritySettings>();
    coreBuilder.AddSwagger(p =>
    {
        p.LoadSettingsFrom(swaggerSettings);
        p.LoadSecuritySettingsFrom(oAuthSettings);
    });

    coreBuilder.AddHealthChecks(p =>
    {

    });
    coreBuilder.AddAutomapper(typeof(Program).Assembly);

}, enforceAuthorization: true);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<PaginationHeadersFilter>();
}).AddNewtonsoftJson(opts =>
{
    opts.SerializerSettings.Converters.Add(
        new StringEnumConverter(new DefaultNamingStrategy(), false));
    opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddCustomAuthentication(builder.Configuration)
    .AddValidatorsFromAssembly(Assembly.Load(Namespace), ServiceLifetime.Scoped);

builder.Host.UseLogging(p =>
{
    p.WithConsoleSink(true);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseValidationExceptionHandler();
app.UseSwagger(swaggerSettings!);

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseCors("CorsAllowAllOrigins");
app.UseAuthentication();

app.MapControllers();

app.UseHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
});

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program
{
    public static string Namespace = typeof(Program).Assembly.GetName().Name;
    public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}
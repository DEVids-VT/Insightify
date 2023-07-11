using FluentValidation;
using HealthChecks.UI.Client;
using Insightify.IdentityAPI.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

var appName = "Identity.API";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddValidatorsFromAssembly(Assembly.Load(Namespace), ServiceLifetime.Scoped);
builder.AddCustomConfiguration();
builder.AddCustomApplicationServices();
builder.AddCustomMvc();
builder.AddCustomHealthChecks();
builder.AddCustomDatabase();
builder.AddCustomIdentity();
builder.AddCustomAuthentication();
builder.AddCustomIdentityServer();
builder.AddEmailSending();

builder.Host.UseSerilog(ProgramExtensions.CreateSerilogLogger(builder.Configuration, appName));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

app.UseRouting();

app.UseIdentityServer();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecks("/liveness", new HealthCheckOptions
{
    Predicate = r => r.Name.Contains("self")
});
app.UseHealthChecksUI(config =>
{
    config.UIPath = "/hc-ui";
});
try
{
    app.Logger.LogInformation("Migrating database for {ApplicationName}...", appName);

    using var scope = app.Services.CreateScope();

    //await DatabaseMiddleware.MigrateDatabase(scope, app.Configuration, app.Logger);

    app.Logger.LogInformation("Starting web host ({ApplicationName})...", appName);
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly ({ApplicationName})...", appName);

    throw;
}
finally
{
    Serilog.Log.CloseAndFlush();
}

[ExcludeFromCodeCoverage]
public partial class Program
{
    public static string Namespace = typeof(Program).Assembly.GetName().Name;
    public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}
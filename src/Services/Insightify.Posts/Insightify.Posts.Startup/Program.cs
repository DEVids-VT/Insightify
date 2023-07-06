using Insightify.Framework.Logging;
using Insightify.Framework.Swagger;
using Insightify.Framework.Swagger.Settings;
using Insightify.Posts.Application;
using Insightify.Posts.Domain;
using Insightify.Posts.Infrastructure;
using Insightify.Posts.Web;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddDomain()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddWebComponents(builder.Configuration)
    .AddCustomAuthentication(builder.Configuration);

builder.Host.UseLogging(p =>
{
    p.WithConsoleSink(true);
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger(builder.Configuration.GetSection("Swagger").Get<SwaggerSettings>()!);

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
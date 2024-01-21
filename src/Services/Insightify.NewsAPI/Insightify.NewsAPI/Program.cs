using Insigghtify.Framework.Mongo.Extensions;
using Insightify.Framework;
using Insightify.Framework.HealthChecks;
using Insightify.Framework.Logging;
using Insightify.Framework.MongoDb.Abstractions.Configuration;
using Insightify.Framework.Swagger;
using Insightify.Framework.Swagger.Settings;
using Insightify.NewsAPI.Extensions;
using Insightify.NewsAPI.Pagination;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

var appName = "NewsAPI";

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory()
});


builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();
var swaggerSettings = builder.Configuration.GetSection("Swagger").Get<SwaggerSettings>();


builder.Services.AddCoreServices(coreBuilder =>
{
    var assembly = typeof(Program).Assembly;
    var mongoSettings = builder.Configuration.GetSection("Mongo").Get<MongoSettings>();
    var oAuthSettings = builder.Configuration.GetSection("SwaggerSecurity").Get<OAuthSecuritySettings>();

    coreBuilder.AddMongoDatabase(p =>
    {
        p.WithConnectionString(mongoSettings.Url);
        p.WithDatabaseName(mongoSettings.Database);
        p.WithSoftDeletes(o =>
        {
            o.Enabled(mongoSettings.SoftDeleteEnabled);
            o.HardDeleteAfter(TimeSpan.FromDays(mongoSettings.SoftDeleteRetentionInDays));
        });
        p.RepresentEnumValuesAs(BsonType.String);
        p.WithIgnoreIfDefaultConvention(false);
        p.WithIgnoreIfNullConvention(true);
    });
    coreBuilder.AddSwagger(p =>
    {
        p.LoadSettingsFrom(swaggerSettings);
        p.LoadSecuritySettingsFrom(oAuthSettings);
    });
    coreBuilder.AddHealthChecks(p =>
    {
        p.AddMongoDb(mongodbConnectionString: mongoSettings.Url, mongoDatabaseName: mongoSettings.Database);
    });

}, enforceAuthorization: true);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<PaginationHeadersFilter>();
})
.AddNewtonsoftJson(opts =>
{
    opts.SerializerSettings.Converters.Add(
        new StringEnumConverter(new DefaultNamingStrategy(), false));
});

builder.Services
    .AddApplicationServices(builder.Configuration)
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

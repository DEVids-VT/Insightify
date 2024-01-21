using FluentValidation;
using HealthChecks.UI.Client;
using Insigghtify.Framework.Mongo.Extensions;
using Insightify.Framework;
using Insightify.Framework.Automapper;
using Insightify.Framework.Fetching.Extensions;
using Insightify.Framework.HealthChecks;
using Insightify.Framework.Messaging.Extensions;
using Insightify.Framework.MongoDb.Abstractions.Configuration;
using Insightify.Framework.Swagger;
using Insightify.Framework.Swagger.Settings;
using Insightify.NewsBackgroundTasks.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Bson;
using Serilog;
using System.Reflection;

var appName = "Insightify.NewsBackgroundTasks";

Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

var builder = WebApplication.CreateBuilder(args);

//builder.AddCustomConfiguration();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddCoreServices(coreBuilder =>
{
    var assembly = typeof(Program).Assembly;

    var mongoDbSettings = builder.Configuration.GetSection("Mongo").Get<MongoSettings>();
    var swaggerSettings = builder.Configuration.GetSection("Swagger").Get<SwaggerSettings>();
    var oAuthSettings = builder.Configuration.GetSection("SwaggerSecurity").Get<OAuthSecuritySettings>();


    coreBuilder.AddMongoDatabase(p =>
    {
        p.WithConnectionString(mongoDbSettings.Url);
        p.WithDatabaseName(mongoDbSettings.Database);
        p.WithSoftDeletes(o =>
        {
            o.Enabled(mongoDbSettings.SoftDeleteEnabled);
            o.HardDeleteAfter(TimeSpan.FromDays(mongoDbSettings.SoftDeleteRetentionInDays));
        });
        p.RepresentEnumValuesAs(BsonType.String);
        p.WithIgnoreIfDefaultConvention(false);
        p.WithIgnoreIfNullConvention(true);
    });
    coreBuilder.AddAutomapper(assembly);
    coreBuilder.AddHealthChecks(p =>
    {
        p.AddMongoDb(mongoDbSettings!.Url);
    });

    coreBuilder.AddApiFetcher(p =>
    {
        var key = builder.Configuration.GetSection("LiveNewsApi").GetValue<string>("Key")!;
        var url = builder.Configuration.GetSection("LiveNewsApi").GetValue<string>("Url")!;
        p.WithName("LiveNewsApi");
        p.WithApiKey(key, "access_key");
        p.WithBaseUrl(url);

    });
    coreBuilder.AddRabbitMQ(p =>
    {
		var url = builder.Configuration.GetSection("Rabbit").GetValue<string>("Url")!;
		p.WithConnectionString(url);
    });

});
builder.AddQuartzNET();
builder.Services.AddValidatorsFromAssembly(Assembly.Load(Namespace), ServiceLifetime.Scoped);

builder.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();

app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true
});


try
{
    Log.Information("Starting web host ({ApplicationContext})...", appName);
    await app.RunAsync();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", appName);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
public partial class Program
{
    public static string Namespace = typeof(Program).Assembly.GetName().Name;
    public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}

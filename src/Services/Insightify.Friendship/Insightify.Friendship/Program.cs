using FluentValidation;
using HealthChecks.UI.Client;
using Insigghtify.Framework.Mongo.Extensions;
using Insightify.Framework;
using Insightify.Framework.Automapper;
using Insightify.Framework.HealthChecks;
using Insightify.Framework.Logging;
using Insightify.Framework.Messaging.Abstractions.Configuration;
using Insightify.Framework.Messaging.Extensions;
using Insightify.Framework.MongoDb.Abstractions.Configuration;
using Insightify.Framework.Swagger;
using Insightify.Framework.Swagger.Settings;
using Insightify.Friendships.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLogging(cfg =>
{
    cfg.WithConsoleSink(true);
});

var swaggerSettings = builder.Configuration.GetSection("Swagger").Get<SwaggerSettings>();

builder.Services.AddCoreServices(coreBuilder =>
{
    var assembly = typeof(Program).Assembly;

    var oAuthSettings = builder.Configuration.GetSection("SwaggerSecurity").Get<OAuthSecuritySettings>();
    var mongoSettings = builder.Configuration.GetSection("Mongo").Get<MongoSettings>();
    var rabbitConnection = builder.Configuration.GetSection("Rabbit").GetConnectionString("Connection");

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
        p.LoadSettingsFrom(swaggerSettings!);
        p.LoadSecuritySettingsFrom(oAuthSettings!);
    });
    coreBuilder.AddAutomapper(assembly);
    coreBuilder.AddHealthChecks(p =>
    {
        p.AddMongoDb(mongoSettings.Url);
    });
    coreBuilder.AddRabbitMQ(p =>
    {
        p.WithConnectionString("rabbitmq://localhost:5672");
    });
});

builder.Services.AddControllers()
.AddNewtonsoftJson(opts =>
{
    opts.SerializerSettings.Converters.Add(
        new StringEnumConverter(new DefaultNamingStrategy(), false));
});

builder.Services.AddScoped<IFriendshipService, FriendshipService>();
builder.Services.AddValidatorsFromAssembly(Assembly.Load(Namespace), ServiceLifetime.Scoped);
builder.Services.AddCors(p => p.AddPolicy("Friends", b => b
                .SetIsOriginAllowed((host) => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()));

var app = builder.Build();

app.UseSwagger(swaggerSettings!);
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("Friends");
app.UseAuthentication();
app.MapDefaultControllerRoute();
app.UseAuthorization();
app.UseHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

try
{
    Log.Information("Starting web host ({ApplicationContext})...", AppName);
    await app.RunAsync();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
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
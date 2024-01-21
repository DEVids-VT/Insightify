using Insightify.FinancialBackgroundTasks.Extensions;
using Insightify.Framework;
using Insightify.Framework.Fetching.Extensions;
using Insightify.Framework.HealthChecks;
using Insightify.Framework.Messaging.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var appName = "Insightify.FinancialBackgroundTasks";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreServices(coreBuilder =>
{
    var assembly = typeof(Program).Assembly;
    var redisConString = builder.Configuration.GetConnectionString("Redis");
    var rabbitMQConString = builder.Configuration.GetConnectionString("RabbitMQ");

    coreBuilder.Services.AddRedis(redisConString);
    coreBuilder.Services.AddQuartzNET();

    coreBuilder.AddHealthChecks(cfg =>
    {
        cfg.AddRedis(redisConString!);
    });

    coreBuilder.AddApiFetcher(cfg =>
    {
        var url = builder.Configuration.GetSection("CoinGecko").GetValue<string>("Url")!;

        cfg.WithName("CoinGecko");
        cfg.WithBaseUrl(url);
    });

    coreBuilder.AddRabbitMQ(cfg =>
    {
        cfg.WithConnectionString(rabbitMQConString);
    });
});
var app = builder.Build();
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
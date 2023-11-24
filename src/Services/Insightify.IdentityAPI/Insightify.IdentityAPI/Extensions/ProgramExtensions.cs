using Insightify.IdentityAPI.Data;
using Insightify.IdentityAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Duende.IdentityServer.Models;
using Insightify.IdentityAPI.Configuration;
using Insightify.IdentityAPI.EmailSending;
using Serilog;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Insightify.IdentityAPI.Services.AccountSettings;

namespace Insightify.IdentityAPI.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ProgramExtensions
    {
        public static void AddCustomConfiguration(this WebApplicationBuilder builder)
        {

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Configuration.AddConfiguration(configBuilder.Build()).Build();

        }
        public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAccountSettingsService, AccountSettingsService>();
        }
        public static void AddCustomAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
        }
        public static void AddCustomDatabase(this WebApplicationBuilder builder)
        {
            var envConnection = builder.Configuration.GetValue<string>("CONNECTION_STRING");

            builder.Services.AddDbContext<IdentityApiDbContext>(
                options => options.UseSqlServer(!string.IsNullOrEmpty(envConnection) ? envConnection : builder.Configuration.GetConnectionString("DefaultConnection")));
        }
        public static void AddCustomMvc(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });

            });
            builder.Services.AddRazorPages();

        }
        public static void AddCustomIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(cfg =>
                {
                    cfg.Password.RequireUppercase = false;
                    cfg.User.RequireUniqueEmail = true;
                    cfg.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<IdentityApiDbContext>()
                .AddDefaultTokenProviders();
        }
        public static Serilog.ILogger CreateSerilogLogger(IConfiguration config, string appName)
        {
            var seqServerUrl = config.GetValue<string>("Logging:Urls:SeqServerUrl") ?? "http://seq";
            var logstashUrl = config.GetValue<string>("Logging:Urls:LogstashUrl") ?? "http://localhost:8080";

            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", appName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq(seqServerUrl)
                .WriteTo.Http(logstashUrl, null)
                .ReadFrom.Configuration(config)
                .CreateLogger();

            return loggerConfig;
        }
        public static void AddCustomHealthChecks(this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks()
                    .AddCheck("self", () => HealthCheckResult.Healthy())
                    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                        name: "IdentityDB-check",
                        tags: new string[] { "IdentityDB" });
        }
        public static void AddCustomIdentityServer(this WebApplicationBuilder builder)
        {
            var identityServerBuilder = builder.Services.AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl = "/login";

                options.IssuerUri = "http://localhost:5001";
                options.Authentication.CookieLifetime = TimeSpan.FromHours(2);
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddInMemoryIdentityResources(IdentityServerConfig.GetResources())
            .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
            .AddInMemoryClients(IdentityServerConfig.GetClients(builder.Configuration))
            .AddAspNetIdentity<ApplicationUser>();

            //remove for production
            identityServerBuilder.AddDeveloperSigningCredential();
        }

        public static void AddEmailSending(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<EmailSendingSettings>(builder.Configuration.GetSection("EmailSending"));
            builder.Services.AddTransient<IMailSender, MailSender>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.Logging.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Insightify.Framework.Logging
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder builder, Action<LoggingConfiguration> configuration)
        {
            var loggingConfig = new LoggingConfiguration();
            configuration(loggingConfig);

            builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithAssemblyName()
                    .Enrich.WithAssemblyVersion();

                if (!string.IsNullOrEmpty(loggingConfig.SeqServerUrl))
                {
                    loggerConfiguration.WriteTo.Seq(loggingConfig.SeqServerUrl);
                }

                if (loggingConfig.UseConsoleSink)
                {
                    loggerConfiguration.WriteTo.Console();
                }

                if (Debugger.IsAttached)
                {
                    loggerConfiguration.MinimumLevel.Override("Microsoft", LogEventLevel.Error);
                    loggerConfiguration.MinimumLevel.Override("System", LogEventLevel.Error);
                    loggerConfiguration.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
                }
            });
            return builder;
        }
    }
}

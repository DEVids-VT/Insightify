using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.Messaging.Abstractions.Configuration;
using Insightify.Framework.Messaging.Abstractions.Interfaces;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client.Exceptions;


namespace Insightify.Framework.Messaging.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static CoreServicesBuilder AddRabbitMQ(this CoreServicesBuilder builder, Action<RabbitConfiguration> configuration)
        {
            builder.Services.AddRabbitMQ(configuration);
            return builder;
        }

        public static void AddRabbitMQ(this IServiceCollection services, Action<RabbitConfiguration> configuration)
        {
            var config = new RabbitConfiguration();
            configuration(config);

            services.Configure(configuration);

            if (string.IsNullOrEmpty(config.ConnectionString))
            {
                throw new ArgumentNullException("", "RabbitMQ Connection String is empty");
            }

            services.AddTransient<IMessagePublisher, MessagePublisher>();
            services.AddMassTransit(x =>
            {
                if (config.ConsumerAssemblies.Any())
                {
                    x.AddConsumers(assemblies: config.ConsumerAssemblies.ToArray());
                }

                if (config.Consumers.Any())
                {
                    x.AddConsumers(config.Consumers.ToArray());
                }

                x.UsingRabbitMq((p, cfg) =>
                {
                    cfg.UseNewtonsoftJsonSerializer();
                    cfg.UseMessageRetry(r =>
                    {
                        r.Immediate(5);
                        r.Ignore<JsonException>();
                        r.Ignore<BrokerUnreachableException>();
                    });
                    cfg.UseScheduledRedelivery(h =>
                    {
                        h.Ignore<JsonException>();
                        h.Ignore<BrokerUnreachableException>();
                        h.Intervals(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(3));
                    });
                    cfg.Host(new Uri(config.ConnectionString), connectionName: config.ClientName, h =>
                    {
                        h.PublisherConfirmation = true;
                    });

                    foreach (var endpointConfig in config.Endpoints)
                    {
                        cfg.ReceiveEndpoint(endpointConfig.QueueName, e =>
                        {
                            foreach (var consumerType in endpointConfig.ConsumerTypes)
                            {
                                e.ConfigureConsumer(p, consumerType);
                            }
                        });
                    }
                });
            });
        }
    }
}

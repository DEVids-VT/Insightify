using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.Messaging.Abstractions.Configuration
{
    public class RabbitConfiguration
    {
        public string? ClientName { get; set; }
        public string? ConnectionString { get; set; }
        public List<Assembly> ConsumerAssemblies { get; } = new();
        public List<Type> Consumers { get; } = new();
        public List<EndpointConfiguration> Endpoints { get; } = new List<EndpointConfiguration>();

        /// <summary>
        /// This property determines how long `Publish()` or `Send()` should block the
        /// thread waiting for a broker's confirmation.
        ///
        /// If null or 0, default timeout will be used.
        /// </summary>
        public int? PublisherTimeoutInMs { get; set; }

        public void WithRabbitSettings(RabbitSettings value)
        {
            this.ClientName = value.ConsumerName;
            this.ConnectionString = value.Url;
            this.PublisherTimeoutInMs = value.PublisherTimeoutInMs;
        }
        public void WithClientName(string? value) => this.ClientName = value;

        public void WithConnectionString(string? value) => this.ConnectionString = value;

        public void WithPublisherTimeoutInMs(int? value) => this.PublisherTimeoutInMs = value;

        public void ScanAssemblyForConsumers(Assembly assembly) => this.ConsumerAssemblies.Add(assembly);

        public void WithConsumer(Type consumerType) => this.Consumers.Add(consumerType);

        public void AddEndpointConfiguration(string queueName, params Type[] consumerTypes)
        {
            var endpoint = Endpoints.FirstOrDefault(e => e.QueueName == queueName);
            if (endpoint == null)
            {
                endpoint = new EndpointConfiguration(queueName);
                Endpoints.Add(endpoint);
            }

            foreach (var consumerType in consumerTypes)
            {
                endpoint.AddConsumer(consumerType);
            }
        }

    }
}

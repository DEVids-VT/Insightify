using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.Messaging.Abstractions.Configuration
{
    public class EndpointConfiguration
    {
        public string QueueName { get; set; }
        public List<Type> ConsumerTypes { get; set; } = new List<Type>();

        public EndpointConfiguration(string queueName)
        {
            QueueName = queueName;
        }

        public void AddConsumer(Type consumerType)
        {
            ConsumerTypes.Add(consumerType);
        }
    }
}

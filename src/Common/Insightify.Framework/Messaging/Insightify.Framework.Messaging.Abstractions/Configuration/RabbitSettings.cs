using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.Messaging.Abstractions.Configuration
{
    public class RabbitSettings
    {
        /// <summary>
        /// Gets or sets the Rabbit Host Url
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the Client Name for this service in Rabbit
        /// </summary>
        public string? ConsumerName { get; set; }

        /// <summary>
        /// This property determines how long `Publish()` or `Send()` should block the
        /// thread waiting for a broker's confirmation.
        ///
        /// If null or 0, default timeout will be used.
        /// </summary>
        public int? PublisherTimeoutInMs { get; set; }
    }
}

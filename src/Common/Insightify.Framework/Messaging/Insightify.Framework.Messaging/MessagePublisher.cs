using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.Messaging.Abstractions.Configuration;
using Insightify.Framework.Messaging.Abstractions.Interfaces;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Insightify.Framework.Messaging
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IBus Bus;
        private readonly int PublisherTimeoutInMs;
        private const int DefaultPublisherTimeoutInMs = 10 * 1000;

        public MessagePublisher(
            IBus bus,
            IOptions<RabbitConfiguration> rabbitConfiguration)
        {
            this.Bus = bus;
            this.PublisherTimeoutInMs = rabbitConfiguration.Value.PublisherTimeoutInMs ?? DefaultPublisherTimeoutInMs;
        }
        public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : class, IMessage
        {
            using var publishTimeoutCancellation = new CancellationTokenSource();
            using var publishCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, publishTimeoutCancellation.Token);

            publishTimeoutCancellation.CancelAfter(this.PublisherTimeoutInMs);

            await this.Bus.Publish(message, publishCancellation.Token);

        }

        public async Task SendAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : class, IMessage
        {
            using var publishTimeoutCancellation = new CancellationTokenSource();
            using var publishCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, publishTimeoutCancellation.Token);

            publishTimeoutCancellation.CancelAfter(this.PublisherTimeoutInMs);

            await this.Bus.Send(message, publishCancellation.Token);
        }
    }
}

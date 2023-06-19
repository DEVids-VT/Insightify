using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.Messaging.Abstractions.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : class, IMessage;
        Task SendAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : class, IMessage;

    }

}

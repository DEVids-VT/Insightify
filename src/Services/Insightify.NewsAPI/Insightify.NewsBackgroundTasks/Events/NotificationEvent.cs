using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.Messaging.Abstractions.Interfaces;

namespace Insightify.NewsBackgroundTasks.Events
{
    public class NotificationEvent : IMessage
    {
        public string Title { get; init; } = null!;
        public string Summary { get; init; } = null!;
        public string Source { get; init; } = null!;
        public DateTime PublishTime { get; init; }
        public string ImageUrl { get; init; }
        public Guid Id { get; init; }
        public DateTime CreationDate { get; init; }
    }
}

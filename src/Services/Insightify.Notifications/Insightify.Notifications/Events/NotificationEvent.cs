using Insightify.Framework.Messaging.Abstractions.Interfaces;

namespace Insightify.NotificationsAPI.Events
{
    public class NotificationEvent : IMessage
    {
        public string Title { get; init; }

        public string Summary { get; init; }

        public string Source { get; init; }

        public DateTime PublishTime { get; init; }

        public string ImageUrl { get; init; }

        public Guid Id { get ; init; }

        public DateTime CreationDate { get; init; }
    }
}

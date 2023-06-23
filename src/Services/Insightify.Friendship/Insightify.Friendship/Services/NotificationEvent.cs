using Insightify.Framework.Messaging.Abstractions.Interfaces;

namespace Insightify.Friendships.Services
{
    public class NotificationEvent : IMessage
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public Guid Id { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
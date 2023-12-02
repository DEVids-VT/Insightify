using Insightify.Framework.MongoDb.Abstractions;
using Insightify.Framework.MongoDb.Abstractions.Attributes;
using Insightify.NotificationsAPI.Constants;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Insightify.NotificationsAPI.Models
{
    [CollectionName("notifications")]
    public class Notification : MongoEntity
    {
        //[BsonRequired]
        //[MinLength(Validation.Notification.TitleMinLength)]
        //[MaxLength(Validation.Notification.TitleMaxLength)]
        public string? Title { get; init; }

        //[BsonRequired]
        //[MinLength(Validation.Notification.DescriptionMinLength)]
        //[MaxLength(Validation.Notification.DescriptionMaxLength)]
        public string? Summary { get; init; }

        public string? Source { get; init; }

        //[BsonRequired] 
        public DateTime? SentDate { get; init; } = DateTime.UtcNow;

        public DateTime? ReadAt { get; init; }

        [BsonIgnore]
        public bool IsRead => ReadAt.HasValue;

        public string? ImageUrl { get; init; }
    }
}

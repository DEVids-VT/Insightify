using Insightify.Framework.MongoDb.Abstractions;
using Insightify.Framework.MongoDb.Abstractions.Attributes;

namespace Insightify.Friendships.Models
{
    [CollectionName("friendships")]
    public class Friendship : MongoEntity
    {
        public string RequesterUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

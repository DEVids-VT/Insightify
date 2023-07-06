using Insightify.Framework.MongoDb.Abstractions;
using Insightify.Framework.MongoDb.Abstractions.Attributes;

namespace Insightify.Friendships.Models
{
    [CollectionName("friend-requests")]
    public class FriendRequest : MongoEntity
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public FriendRequestStatus Status { get; set; }
    }
}
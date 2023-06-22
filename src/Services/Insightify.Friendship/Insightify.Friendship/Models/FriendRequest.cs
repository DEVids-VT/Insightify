namespace Insightify.Friendship.Models
{
    public class FriendRequest
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public FriendRequestStatus Status { get; set; }
    }
}
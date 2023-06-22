namespace Insightify.Friendship.Models.Dtos
{
    public class FriendshipDto
    {
        public string Id { get; set; }
        public string RequesterUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

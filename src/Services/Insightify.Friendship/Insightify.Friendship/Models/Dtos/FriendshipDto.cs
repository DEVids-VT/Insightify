namespace Insightify.Friendship.Models.Dtos
{
    public class FriendshipDto
    {
        public int Id { get; set; }
        public string RequesterUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

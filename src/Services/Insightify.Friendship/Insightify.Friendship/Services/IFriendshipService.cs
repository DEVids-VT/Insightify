using Insightify.Friendship.Models.Dtos;

namespace Insightify.Friendship.Services
{
    public interface IFriendshipService
    {
        Task<bool> AcceptFriendRequest(string requestId);
        Task<IEnumerable<FriendDto>> GetFriends(string userId);
        Task RejectFriendRequest(string requestId);
        Task<bool> SendFriendRequest(string senderId, string receiverId);
        Task<bool> Unfriend(string friendshipId);
    }
}

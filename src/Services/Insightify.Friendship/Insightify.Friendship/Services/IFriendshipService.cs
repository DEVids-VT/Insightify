using Insightify.Friendships.Models;
using Insightify.Friendships.Models.Dtos;

namespace Insightify.Friendships.Services
{
    public interface IFriendshipService
    {
        Task AcceptFriendRequest(string requestId);
        Task<IEnumerable<FriendRequest>> GetRequests(string userId, bool includeDeleted = false);
        Task RejectFriendRequest(string requestId);
        Task SendFriendRequest(string senderId, string receiverId);
        Task Unfriend(string friendshipId);
        Task<IEnumerable<Friendship>> GetFriendships(string userId, bool includeDeleted = false);
        Task<IEnumerable<FriendRequest>> AllRequests();
        Task<IEnumerable<Friendship>> AllFriendships();
    }
}

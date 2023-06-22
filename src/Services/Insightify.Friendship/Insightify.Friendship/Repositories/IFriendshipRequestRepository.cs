using Insightify.Friendship.Models;

namespace Insightify.Friendship.Repositories
{
    public interface IFriendshipRequestRepository
    {
        Task<FriendRequest> GetById(string requestId);
        Task<FriendRequest> Update(FriendRequest friendRequest);
        Task Create(FriendRequest friendRequest);
    }
}
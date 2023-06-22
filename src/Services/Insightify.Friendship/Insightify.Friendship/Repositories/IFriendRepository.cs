using Insightify.Friendship.Controllers;
using Insightify.Friendship.Models;
using Insightify.Friendship.Models.Dtos;

namespace Insightify.Friendship.Repositories
{
    public interface IFriendRepository
    {
        Task AddFriend(string senderId, string receiverId);
        Task<FriendshipDto> GetById(string friendshipId);
        Task<IEnumerable<FriendDto>> GetFriends(string userId);
        Task<bool> RemoveFriendship(FriendshipDto friendship);
    }
}
using Insightify.Friendship.Models;
using Insightify.Friendship.Models.Dtos;
using Insightify.Friendship.Repositories;

namespace Insightify.Friendship.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRequestRepository _friendshipRequestRepository;
        private readonly IFriendRepository _friendRepository;

        public FriendshipService(IFriendshipRequestRepository friendshipRequestRepository, IFriendRepository friendRepository)
        {
            _friendshipRequestRepository = friendshipRequestRepository;
            _friendRepository = friendRepository;
        }

        public async Task<bool> SendFriendRequest(string senderId, string receiverId)
        {
            var friendRequest = new FriendRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Status = FriendRequestStatus.Pending
            };

            try
            {
                await _friendshipRequestRepository.Create(friendRequest);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AcceptFriendRequest(string requestId)
        {
            var friendRequest = await _friendshipRequestRepository.GetById(requestId);

            friendRequest.Status = FriendRequestStatus.Accepted;

            var updatedRequest = await _friendshipRequestRepository.Update(friendRequest);

            await _friendRepository.AddFriend(updatedRequest.SenderId, updatedRequest.ReceiverId);
            await _friendRepository.AddFriend(updatedRequest.ReceiverId, updatedRequest.SenderId);

            return updatedRequest != null;
        }

        public async Task RejectFriendRequest(string requestId)
        {
            var friendRequest = await _friendshipRequestRepository.GetById(requestId);

            friendRequest.Status = FriendRequestStatus.Rejected;

            await _friendshipRequestRepository.Update(friendRequest);
        }

        public async Task<bool> Unfriend(string friendshipId)
        {
            var friendship = await _friendRepository.GetById(friendshipId);

            var removedFriendship = await _friendRepository.RemoveFriendship(friendship);

            return removedFriendship;
        }

        public async Task<IEnumerable<FriendDto>> GetFriends(string userId)
        {
            var friends = await _friendRepository.GetFriends(userId);

            return friends;
        }
    }
}

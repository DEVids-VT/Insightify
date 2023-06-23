using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Insightify.Friendships.Models;
using Insightify.Friendships.Models.Dtos;
using k8s.KubeConfigModels;

namespace Insightify.Friendships.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IRepository<FriendRequest> _friendRequestRepo;
        private readonly IRepository<Friendship> _friendshipRepo;

        public FriendshipService(IRepository<FriendRequest> friendRequestRepo, IRepository<Friendship> friendshipRepo)
        {
            _friendRequestRepo = friendRequestRepo;
            _friendshipRepo = friendshipRepo;
        }

        public async Task SendFriendRequest(string senderId, string receiverId)
        {
            var friendRequest = new FriendRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Status = FriendRequestStatus.Pending
            };

            await _friendRequestRepo.InsertAsync(friendRequest);
        }

        public async Task AcceptFriendRequest(string requestId)
        {
            var friendRequest = await _friendRequestRepo.GetByIdAsync(requestId);

            if(friendRequest == null)
            {
                throw new ArgumentException("Friend request not found.");
            }

            friendRequest.Status = FriendRequestStatus.Accepted;

            await _friendRequestRepo.UpdateAsync(friendRequest);

            await _friendshipRepo.InsertAsync(new Friendship
            {
                RequesterUserId = friendRequest.SenderId,
                ReceiverUserId = friendRequest.ReceiverId,
                CreatedAt = DateTime.Now
            });
        }

        public async Task RejectFriendRequest(string requestId)
        {
            var friendRequest = await _friendRequestRepo.GetByIdAsync(requestId);

            if (friendRequest == null)
            {
                throw new ArgumentException("Friend request not found.");
            }

            friendRequest.Status = FriendRequestStatus.Rejected;

            await _friendRequestRepo.UpdateAsync(friendRequest);
        }

        public async Task Unfriend(string friendshipId)
        {
            var friendship = await _friendshipRepo.GetByIdAsync(friendshipId);

            if (friendship == null)
            {
                throw new ArgumentException("Friend request not found.");
            }

            await _friendshipRepo.DeleteAsync(friendship);
        }

        public async Task<IEnumerable<FriendRequest>> GetRequests(string userId, bool includeDeleted = false)
        {
            var requests = await _friendRequestRepo.GetAllAsync(x => x.SenderId == userId || x.ReceiverId == userId, includeDeleted: includeDeleted);

            return requests.AsEnumerable();
        }

        public async Task<IEnumerable<Friendship>> GetFriendships(string userId, bool includeDeleted = false)
        {
            var friends = await _friendshipRepo.GetAllAsync(x => x.RequesterUserId == userId || x.ReceiverUserId == userId, includeDeleted: includeDeleted);

            return friends.AsEnumerable();
        }

        public async Task<IEnumerable<FriendRequest>> AllRequests(bool includeDeleted = false)
        {
            var requests = await _friendRequestRepo.GetAllAsync(includeDeleted: includeDeleted);

            return requests.AsEnumerable();
        }

        public async Task<IEnumerable<Friendship>> AllFriendships(bool includeDeleted = false)
        {
            var friendships = await _friendshipRepo.GetAllAsync(includeDeleted: includeDeleted);

            return friendships.AsEnumerable();
        }
    }
}

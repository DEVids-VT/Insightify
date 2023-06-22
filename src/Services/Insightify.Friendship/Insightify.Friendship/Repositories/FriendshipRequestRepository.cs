using Insightify.Friendship.Models;
using MongoDB.Driver;

namespace Insightify.Friendship.Repositories
{
    public class FriendshipRequestRepository : IFriendshipRequestRepository
    {
        private readonly IMongoCollection<FriendRequest> _friendRequestCollection;

        public FriendshipRequestRepository(IMongoDatabase database)
        {
            _friendRequestCollection = database.GetCollection<FriendRequest>("friendRequests");
        }

        public async Task<FriendRequest> GetById(string requestId)
        {
            var filter = Builders<FriendRequest>.Filter.Eq(fr => fr.Id, requestId);
            return await _friendRequestCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<FriendRequest> Update(FriendRequest friendRequest)
        {
            var filter = Builders<FriendRequest>.Filter.Eq(fr => fr.Id, friendRequest.Id);
            return await _friendRequestCollection.FindOneAndReplaceAsync(filter, friendRequest);
        }

        async Task IFriendshipRequestRepository.Create(FriendRequest friendRequest)
        {
            await _friendRequestCollection.InsertOneAsync(friendRequest);
        }
    }
}

using Insightify.Friendship.Models.Dtos;
using MongoDB.Driver;

namespace Insightify.Friendship.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly IMongoCollection<FriendshipDto> _friendshipCollection;

        public FriendRepository(IMongoDatabase database)
        {
            _friendshipCollection = database.GetCollection<FriendshipDto>("friendships");
        }

        public async Task AddFriend(string senderId, string receiverId)
        {
            var friendship = new FriendshipDto
            {
                RequesterUserId = senderId,
                ReceiverUserId = receiverId,
                CreatedAt = DateTime.UtcNow
            };

            await _friendshipCollection.InsertOneAsync(friendship);
        }

        public async Task<FriendshipDto> GetById(int friendshipId)
        {
            var filter = Builders<FriendshipDto>.Filter.Eq(f => f.Id, friendshipId);
            return await _friendshipCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FriendDto>> GetFriends(string userId)
        {
            var filter = Builders<FriendshipDto>.Filter.Or(
                Builders<FriendshipDto>.Filter.Eq(f => f.RequesterUserId, userId),
                Builders<FriendshipDto>.Filter.Eq(f => f.ReceiverUserId, userId)
            );

            var friendships = await _friendshipCollection.Find(filter).ToListAsync();

            var friends = new List<FriendDto>();
            foreach (var friendship in friendships)
            {
                string friendUserId = friendship.RequesterUserId == userId
                    ? friendship.ReceiverUserId
                    : friendship.RequesterUserId;

                string friendUserName = GetFriendUserName(friendUserId);

                var friend = new FriendDto
                {
                    UserId = friendUserId,
                    UserName = friendUserName
                };

                friends.Add(friend);
            }

            return friends;
        }

        public async Task<bool> RemoveFriendship(FriendshipDto friendship)
        {
            var filter = Builders<FriendshipDto>.Filter.Eq(f => f.Id, friendship.Id);
            var result = await _friendshipCollection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        private string GetFriendUserName(string friendUserId)
        {
            return "Friend's Name";
        }
    }
}

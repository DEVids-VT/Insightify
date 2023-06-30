using Insightify.Framework.Messaging.Abstractions.Interfaces;
using Insightify.Framework.MongoDb.Abstractions.Enums;
using System.Linq.Expressions;

namespace Insightify.Friendships.Tests
{
    public class FriendshipServiceTests
    {
        private Mock<IRepository<FriendRequest>> _friendRepo;
        private Mock<IRepository<Friendship>> _friendshipRepo;
        private IFriendshipService _friendshipService;

        public FriendshipServiceTests()
        {
            _friendRepo = new Mock<IRepository<FriendRequest>>();
            _friendshipRepo = new Mock<IRepository<Friendship>>();
            _friendshipService = new FriendshipService(_friendRepo.Object, _friendshipRepo.Object, new Mock<IMessagePublisher>().Object);
        }

        [Fact]
        public async Task SendFriendRequest_Should_Create_A_Request_And_Add_To_The_DatabaseAsync()
        {
            // Arrange
            string senderId = "senderId";
            string receiverId = "receiverId";

            _friendRepo
                .Setup(r => r.InsertAsync(It.IsAny<FriendRequest>()))
                .Returns(Task.CompletedTask);

            // Act
            await _friendshipService.SendFriendRequest(senderId, receiverId);

            // Assert
            _friendRepo.Verify(r => r.InsertAsync(It.IsAny<FriendRequest>()), Times.Once);
        }

        [Fact]
        public async Task AcceptFriendRequest_Should_Update_Status_And_Add_Friendship()
        {
            // Arrange
            string requestId = "requestId";
            var friendRequest = new FriendRequest
            {
                Id = requestId,
                SenderId = "senderId",
                ReceiverId = "receiverId",
                Status = FriendRequestStatus.Pending
            };
            var friendship = new Friendship
            {
                RequesterUserId = friendRequest.SenderId,
                ReceiverUserId = friendRequest.ReceiverId,
                CreatedAt = DateTime.Now
            };

            _friendRepo
                .Setup(r => r.GetByIdAsync(requestId))
                .ReturnsAsync(friendRequest);
            _friendRepo
                .Setup(r => r.UpdateAsync(friendRequest))
                .Returns(Task.CompletedTask);
            _friendshipRepo
                .Setup(r => r.InsertAsync(friendship))
                .Returns(Task.CompletedTask);

            // Act
            await _friendshipService.AcceptFriendRequest(requestId);

            // Assert
            _friendRepo.Verify(r => r.GetByIdAsync(requestId), Times.Once);
            _friendRepo.Verify(r => r.UpdateAsync(friendRequest), Times.Once);
            _friendshipRepo.Verify(r => r.InsertAsync(friendship), Times.Once);
            Assert.Equal(FriendRequestStatus.Accepted, friendRequest.Status);
        }

        [Fact]
        public async Task AcceptFriendRequest_Should_Throw_Exception_When_Request_Not_Found()
        {
            // Arrange
            string requestId = "requestId";
            FriendRequest nullFriendRequest = null;

            _friendRepo
                .Setup(r => r.GetByIdAsync(requestId))
                .ReturnsAsync(nullFriendRequest);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _friendshipService.AcceptFriendRequest(requestId));
            _friendRepo.Verify(r => r.GetByIdAsync(requestId), Times.Once);
        }

        [Fact]
        public async Task RejectFriendRequest_Should_Update_Status()
        {
            // Arrange
            string requestId = "requestId";
            var friendRequest = new FriendRequest
            {
                Id = requestId,
                Status = FriendRequestStatus.Pending
            };

            _friendRepo
                .Setup(r => r.GetByIdAsync(requestId))
                .ReturnsAsync(friendRequest);
            _friendRepo
                .Setup(r => r.UpdateAsync(friendRequest))
                .Returns(Task.CompletedTask);

            // Act
            await _friendshipService.RejectFriendRequest(requestId);

            // Assert
            _friendRepo.Verify(r => r.GetByIdAsync(requestId), Times.Once);
            _friendRepo.Verify(r => r.UpdateAsync(friendRequest), Times.Once);
            Assert.Equal(FriendRequestStatus.Rejected, friendRequest.Status);
        }

        [Fact]
        public async Task RejectFriendRequest_Should_Throw_Exception_When_Request_Not_Found()
        {
            // Arrange
            string requestId = "requestId";
            FriendRequest nullFriendRequest = null;

            _friendRepo
                .Setup(r => r.GetByIdAsync(requestId))
                .ReturnsAsync(nullFriendRequest);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _friendshipService.RejectFriendRequest(requestId));
            _friendRepo.Verify(r => r.GetByIdAsync(requestId), Times.Once);
        }

        [Fact]
        public async Task Unfriend_Should_Delete_Friendship()
        {
            // Arrange
            string friendshipId = "friendshipId";
            var friendship = new Friendship
            {
                Id = friendshipId
            };

            _friendshipRepo
                .Setup(r => r.GetByIdAsync(friendshipId))
                .ReturnsAsync(friendship);
            _friendshipRepo
                .Setup(r => r.DeleteAsync(friendship, false))
                .Returns(Task.CompletedTask);

            // Act
            await _friendshipService.Unfriend(friendshipId);

            // Assert
            _friendshipRepo.Verify(r => r.GetByIdAsync(friendshipId), Times.Once);
            _friendshipRepo.Verify(r => r.DeleteAsync(friendship, false), Times.Once);
        }

        [Fact]
        public async Task Unfriend_Should_Throw_Exception_When_Friendship_Not_Found()
        {
            // Arrange
            string friendshipId = "friendshipId";
            Friendship nullFriendship = null;

            _friendshipRepo
                .Setup(r => r.GetByIdAsync(friendshipId))
                .ReturnsAsync(nullFriendship);

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _friendshipService.Unfriend(friendshipId));
            _friendshipRepo.Verify(r => r.GetByIdAsync(friendshipId), Times.Once);
        }

        [Fact]
        public async Task GetRequests_Should_Return_Requests_For_User()
        {
            // Arrange
            string userId = "userId";
            bool includeDeleted = false;
            var requests = new List<FriendRequest>
            {
                new FriendRequest { SenderId = userId },
                new FriendRequest { ReceiverId = userId },
                new FriendRequest { SenderId = "otherId" }
            };

            // Assuming you have a mock repository instance named "_friendRepo"
            _friendRepo
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<FriendRequest, bool>>>(),
                    It.IsAny<Expression<Func<FriendRequest, object>>>(),
                    It.IsAny<SortDirection>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(requests);

            // Act
            var result = await _friendshipService.GetRequests(userId, includeDeleted);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(requests[0], result);
            Assert.Contains(requests[1], result);
            Assert.DoesNotContain(requests[2], result);
        }

        [Fact]
        public async Task AllRequests_Should_Return_All_Requests()
        {
            // Arrange
            bool includeDeleted = false;
            var requests = new List<FriendRequest> {
            new FriendRequest(),
            new FriendRequest()
        };

            _friendRepo
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<FriendRequest, bool>>>(),
                    It.IsAny<Expression<Func<FriendRequest, object>>>(),
                    It.IsAny<SortDirection>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(requests);

            // Act
            var result = await _friendshipService.AllRequests(includeDeleted);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(requests[0], result);
            Assert.Contains(requests[1], result);
        }

        [Fact]
        public async Task GetFriendships_IncludeDeletedFalse_ReturnsFriendships()
        {
            // Arrange
            string userId = "user123";
            bool includeDeleted = false;

            List<Friendship> friendships = new List<Friendship>
            {
                new Friendship { RequesterUserId = "user123", ReceiverUserId = "user456", CreatedAt = DateTime.Now.AddDays(-1) },
                new Friendship { RequesterUserId = "user789", ReceiverUserId = "user123", CreatedAt = DateTime.Now.AddDays(-2) }
            };

            _friendshipRepo.Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<Friendship, bool>>>(),
                    It.IsAny<Expression<Func<Friendship, object>>>(),
                    It.IsAny<SortDirection>(),
                    includeDeleted))
                .ReturnsAsync(friendships);

            // Act
            var result = await _friendshipService.GetFriendships(userId, includeDeleted);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(friendships, result);
        }

        [Fact]
        public async Task AllFriendships_Should_Return_All_Friendships()
        {
            // Arrange
            bool includeDeleted = false;
            var friendships = new List<Friendship> {
            new Friendship(),
            new Friendship()
        };

            _friendshipRepo
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<Friendship, bool>>>(),
                    It.IsAny<Expression<Func<Friendship, object>>>(),
                    It.IsAny<SortDirection>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(friendships);

            // Act
            var result = await _friendshipService.AllFriendships(includeDeleted);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(friendships[0], result);
            Assert.Contains(friendships[1], result);
        }
    }
}
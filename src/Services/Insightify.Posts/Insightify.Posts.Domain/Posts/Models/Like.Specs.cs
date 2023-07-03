namespace Insightify.Posts.Domain.Posts.Models
{
    using FluentAssertions;
    using Xunit;
    public class LikeSpecs
    {
        [Fact]
        public void Like_Initialization_Works()
        {
            //Arrange
            string userId = "ValidUserId";
            DateTime timestamp = DateTime.Now;

            //Act
            var like = new Like(userId, timestamp);

            //Assert
            like.UserId.Should().BeEquivalentTo(userId);
            like.Timestamp.Should().Be(timestamp);
        }
    }
}

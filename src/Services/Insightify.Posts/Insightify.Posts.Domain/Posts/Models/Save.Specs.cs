namespace Insightify.Posts.Domain.Posts.Models
{
    using FluentAssertions;
    using Xunit;
    public class SaveSpecs
    {
        [Fact]
        public void Save_Initialization_Works()
        {
            //Arrange
            string userId = "ValidUserId";
            DateTime timestamp = DateTime.Now;

            //Act
            var save = new Save(userId, timestamp);

            //Assert
            save.UserId.Should().BeEquivalentTo(userId);
            save.Timestamp.Should().Be(timestamp);
        }
    }
}

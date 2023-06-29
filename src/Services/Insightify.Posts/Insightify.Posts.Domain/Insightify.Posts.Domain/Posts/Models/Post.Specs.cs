namespace Insightify.Posts.Domain.Posts.Models
{
    using FluentAssertions;
    using Insightify.Posts.Domain.Posts.Exceptions;
    using Xunit;

    public class PostSpecs
    {
        [Fact]
        public void Post_Initialization_Works()
        {
            //Arrange
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";

            //Act
            var post = new Post(title, description, authorId, null);

            //Assert
            post.Title.Should().BeEquivalentTo(title);
            post.Description.Should().BeEquivalentTo(description);
            post.AuthorId.Should().BeEquivalentTo(authorId);
        }

        [Fact]
        public void Valid_Title_Should_Not_Throw_Exception()
        {
            //Arrange
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";

            //Act
            Action act = () => new Post(title, description, authorId, null);

            //Assert
            act.Should().NotThrow<InvalidPostException>();
        }

        [Fact]
        public void Invalid_Title_Should_Throw_Exception()
        {
            //Arrange
            string title = "";
            string description = "ValidDescription";
            string authorId = "ValidUserId";

            //Act
            Action act = () => new Post(title, description, authorId, null);

            //Assert
            act.Should().Throw<InvalidPostException>();
        }

        [Fact]
        public void Add_Comment_Should_Increase_TotalComments_By_One()
        {
            //Arrange
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var post = new Post(title, description, authorId, null);
            string commentContent = "ValidComment";
            string commentUserId = "ValidUserId";

            //Act
            post.AddComment(commentContent, commentUserId);

            //Assert
            post.TotalComments.Should().Be(1);
        }

        [Fact]
        public void Add_Like_Should_Increase_TotalLikes_By_One()
        {
            //Arrange
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var post = new Post(title, description, authorId, null);
            string likeUserId = "ValidUserId";

            //Act
            post.AddLike(likeUserId, DateTime.Now);

            //Assert
            post.TotalLikes.Should().Be(1);
        }

        [Fact]
        public void Add_Save_Should_Increase_TotalSaves_By_One()
        {
            //Arrange
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var post = new Post(title, description, authorId, null);
            string saveUserId = "ValidUserId";

            //Act
            post.AddSave(saveUserId, DateTime.Now);

            //Assert
            post.TotalSaves.Should().Be(1);
        }

        [Fact]
        public void Remove_Like_Should_Decrease_TotalLikes_By_One()
        {
            //Arrange
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var post = new Post(title, description, authorId, null);
            string likeUserId = "ValidUserId";
            post.AddLike(likeUserId, DateTime.Now);
            var likeId = post.Likes.First().Id;

            //Act
            post.RemoveLike(likeId);

            //Assert
            post.TotalLikes.Should().Be(0);
        }

        [Fact]
        public void Remove_Save_Should_Decrease_TotalSaves_By_One()
        {
            //Arrange
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var post = new Post(title, description, authorId, null);
            string saveUserId = "ValidUserId";
            post.AddSave(saveUserId, DateTime.Now);
            var saveId = post.Saves.First().Id;

            //Act
            post.RemoveSave(saveId);

            //Assert
            post.TotalSaves.Should().Be(0);
        }

        [Fact]
        public void Remove_Comment_Should_Decrease_TotalComments_By_One()
        {
            //Arrange
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var post = new Post(title, description, authorId, null);
            string commentContent = "ValidComment";
            string commentUserId = "ValidUserId";
            post.AddComment(commentContent, commentUserId);
            var commentId = post.Comments.First().Id;

            //Act
            post.RemoveComment(commentId);

            //Assert
            post.TotalComments.Should().Be(0);
        }

        [Fact]
        public void Update_Title_Should_Change_Title()
        {
            //Arrange
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var post = new Post(title, description, authorId, null);
            string newTitle = "NewValidTitle";

            //Act
            post.UpdateTitle(newTitle);

            //Assert
            post.Title.Should().BeEquivalentTo(newTitle);
        }

        [Fact]
        public void Update_Description_Should_Change_Description()
        {
            //Arrange
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var post = new Post(title, description, authorId, null);
            string newDescription = "NewValidDescription";

            //Act
            post.UpdateDescription(newDescription);

            //Assert
            post.Description.Should().BeEquivalentTo(newDescription);
        }

        [Fact]
        public void Update_ImageUrl_Should_Change_ImageUrl()
        {
            //Arrange
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            string imageUrl = "http://validurl.com";
            var post = new Post(title, description, authorId, imageUrl);
            string newImageUrl = "http://newvalidurl.com";

            //Act
            post.UpdateImageUrl(newImageUrl);

            //Assert
            post.ImageUrl.Should().BeEquivalentTo(newImageUrl);
        }
    }
}

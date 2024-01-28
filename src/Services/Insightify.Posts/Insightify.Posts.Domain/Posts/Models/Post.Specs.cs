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
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var tags = new List<Tag> { new Tag("Tag1") };
            var post = new Post(title, description, authorId, null, tags);
            post.Title.Should().BeEquivalentTo(title);
            post.Description.Should().BeEquivalentTo(description);
            post.AuthorId.Should().BeEquivalentTo(authorId);
            post.Tags.Should().BeEquivalentTo(tags);
        }

        [Fact]
        public void Valid_Title_Should_Not_Throw_Exception()
        {
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var tags = new List<Tag> { new Tag("Tag1") };
            Action act = () => new Post(title, description, authorId, null, tags);
            act.Should().NotThrow<InvalidPostException>();
        }

        [Fact]
        public void Invalid_Title_Should_Throw_Exception()
        {
            string title = "";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var tags = new List<Tag> { new Tag("Tag1") };
            Action act = () => new Post(title, description, authorId, null, tags);
            act.Should().Throw<InvalidPostException>();
        }

        [Fact]
        public void Add_Comment_Should_Increase_TotalComments_By_One()
        {
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var tags = new List<Tag> { new Tag("Tag1") };
            var post = new Post(title, description, authorId, null, tags);
            string commentContent = "ValidComment";
            string commentUserId = "ValidUserId";
            post.AddComment(commentContent, commentUserId);
            post.TotalComments.Should().Be(1);
        }

        [Fact]
        public void Add_Like_Should_Increase_TotalLikes_By_One()
        {
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var tags = new List<Tag> { new Tag("Tag1") };
            var post = new Post(title, description, authorId, null, tags);
            string likeUserId = "ValidUserId";
            post.AddLike(likeUserId, DateTime.Now);
            post.TotalLikes.Should().Be(1);
        }

        [Fact]
        public void Add_Save_Should_Increase_TotalSaves_By_One()
        {
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var tags = new List<Tag> { new Tag("Tag1") };
            var post = new Post(title, description, authorId, null, tags);
            string saveUserId = "ValidUserId";
            post.AddSave(saveUserId, DateTime.Now);
            post.TotalSaves.Should().Be(1);
        }

        [Fact]
        public void Remove_Like_Should_Decrease_TotalLikes_By_One()
        {
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var tags = new List<Tag> { new Tag("Tag1") };
            var post = new Post(title, description, authorId, null, tags);
            string likeUserId = "ValidUserId";
            post.AddLike(likeUserId, DateTime.Now);
            var likeId = post.Likes.First().Id;
            post.RemoveLike(likeId);
            post.TotalLikes.Should().Be(0);
        }

        [Fact]
        public void Remove_Save_Should_Decrease_TotalSaves_By_One()
        {
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var tags = new List<Tag> { new Tag("Tag1") };
            var post = new Post(title, description, authorId, null, tags);
            string saveUserId = "ValidUserId";
            post.AddSave(saveUserId, DateTime.Now);
            var saveId = post.Saves.First().Id;
            post.RemoveSave(saveId);
            post.TotalSaves.Should().Be(0);
        }

        [Fact]
        public void Remove_Comment_Should_Decrease_TotalComments_By_One()
        {
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var tags = new List<Tag> { new Tag("Tag1") };
            var post = new Post(title, description, authorId, null, tags);
            string commentContent = "ValidComment";
            string commentUserId = "ValidUserId";
            post.AddComment(commentContent, commentUserId);
            var commentId = post.Comments.First().Id;
            post.RemoveComment(commentId);
            post.TotalComments.Should().Be(0);
        }

        [Fact]
        public void Update_Title_Should_Change_Title()
        {
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var tags = new List<Tag> { new Tag("Tag1") };
            var post = new Post(title, description, authorId, null, tags);
            string newTitle = "NewValidTitle";
            post.UpdateTitle(newTitle);
            post.Title.Should().BeEquivalentTo(newTitle);
        }

        [Fact]
        public void Update_Description_Should_Change_Description()
        {
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            var tags = new List<Tag> { new Tag("Tag1") };
            var post = new Post(title, description, authorId, null, tags);
            string newDescription = "NewValidDescription";
            post.UpdateDescription(newDescription);
            post.Description.Should().BeEquivalentTo(newDescription);
        }

        [Fact]
        public void Update_ImageUrl_Should_Change_ImageUrl()
        {
            string title = "ValidTitle";
            string description = "ValidDescription";
            string authorId = "ValidUserId";
            string imageUrl = "http://validurl.com";
            var tags = new List<Tag> { new Tag("Tag1") };
            var post = new Post(title, description, authorId, imageUrl, tags);
            string newImageUrl = "http://newvalidurl.com";
            post.UpdateImageUrl(newImageUrl);
            post.ImageUrl.Should().BeEquivalentTo(newImageUrl);
        }
    }
}

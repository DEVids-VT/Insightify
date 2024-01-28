using Insightify.Posts.Domain.Posts.Models;

namespace Insightify.Posts.Domain.Posts.Factories
{
    using FluentAssertions;
    using Insightify.Posts.Domain.Posts.Exceptions;
    using Xunit;
    public class PostFactorySpecs
    {
        [Fact]
        public void BuildShouldThrowExceptionIfTitleIsNotSet()
        {
            // Arrange
            var postFactory = new PostFactory();

            // Act
            Action act = () => postFactory
                .WithDescription("TestDescription")
                .WithAuthor("TestAuthorId")
                .Build();

            // Assert
            act.Should().Throw<InvalidPostException>();
        }

        [Fact]
        public void BuildShouldThrowExceptionIfDescriptionIsNotSet()
        {
            // Arrange
            var postFactory = new PostFactory();

            // Act
            Action act = () => postFactory
                .WithTitle("TestTitle")
                .WithAuthor("TestAuthorId")
                .Build();

            // Assert
            act.Should().Throw<InvalidPostException>();
        }

        [Fact]
        public void BuildShouldThrowExceptionIfAuthorIsNotSet()
        {
            // Arrange
            var postFactory = new PostFactory();

            // Act
            Action act = () => postFactory
                .WithTitle("TestTitle")
                .WithDescription("TestDescription")
                .Build();

            // Assert
            act.Should().Throw<InvalidPostException>();
        }

        [Fact]
        public void BuildShouldCreatePostIfEveryPropertyIsSet()
        {
            // Arrange
            var postFactory = new PostFactory();

            // Act
            var post = postFactory
                .WithTitle("TestTitle")
                .WithDescription("TestDescription")
                .WithAuthor("TestAuthorId")
                .WithImageUrl("http://test.image.url")
                .WithTags(new List<Tag>())
                .Build();

            // Assert
            post.Should().NotBeNull();
        }
    }
}


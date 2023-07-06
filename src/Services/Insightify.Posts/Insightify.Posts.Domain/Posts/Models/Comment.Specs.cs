using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Insightify.Posts.Domain.Posts.Exceptions;
using Xunit;

namespace Insightify.Posts.Domain.Posts.Models
{
    public class CommentSpecs
    {
        [Fact]
        public void Comment_Initialization_Works()
        {
            //Arrange
            string content = "ValidContent";
            string userId = "ValidUserId";

            //Act
            var comment = new Comment(content, userId);

            //Assert
            comment.AuthorId.Should().BeEquivalentTo(userId);
            comment.Content.Should().BeEquivalentTo(content);
        }
        [Fact]
        public void Valid_Content_Should_Not_Throw_Exception()
        {
            //Arrange
            string content = "ValidContent";
            string userId = "ValidUserIds";

            //Act
            Action act = () => new Comment(content, userId);

            //Assert
            act.Should().NotThrow<InvalidCommentException>();
        }

        [Fact]
        public void Invalid_Content_Should_Throw_Exception()
        {
            //Arrange
            string content = "";
            string userId = "ValidUserIds";

            //Act
            Action act = () => new Comment(content, userId);

            //Assert
            act.Should().Throw<InvalidCommentException>();
        }
    }
}

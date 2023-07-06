
namespace Insightify.Posts.Domain.Posts.Models
{
    using Insightify.Posts.Domain.Common.Models;
    using Insightify.Posts.Domain.Posts.Exceptions;

    using static ModelConstants.Comment;

    public class Comment : Entity<int>
    {

        internal Comment(string content, string authorId)
        {
            this.ValidateContent(content);

            this.Content = content;
            this.AuthorId = authorId;
        }
        public string AuthorId { get; }
        public string Content { get; private set; }

        public Comment UpdateContent(string content)
        {
            this.ValidateContent(content);
            this.Content = content;

            return this;
        }

        private void ValidateContent(string content)
            => Guard.ForStringLength<InvalidCommentException>(
                content,
                MinContentLength,
                MaxContentLength,
                nameof(this.Content));
    }
}

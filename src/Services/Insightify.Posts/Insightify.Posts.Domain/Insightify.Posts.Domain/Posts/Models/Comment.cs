
namespace Insightify.Posts.Domain.Posts.Models
{
    using Insightify.Posts.Domain.Common.Models;
    using Insightify.Posts.Domain.Posts.Exceptions;

    using static ModelConstants.Comment;

    public class Comment : Entity<Guid>
    {
        private readonly HashSet<Comment> comments;

        internal Comment(string content, string authorId)
        {
            this.ValidateContent(content);

            this.Content = content;
            this.AuthorId = authorId;

            comments = new HashSet<Comment>();
        }
        public string AuthorId { get; }
        public string Content { get; private set; }

        public IReadOnlyCollection<Comment> Comments => comments.ToList().AsReadOnly();
        public int TotalComments => comments.Count;

        public Comment UpdateContent(string content)
        {
            this.ValidateContent(content);
            this.Content = content;

            return this;
        }

        public Comment AddComment(string content, string userId)
        {
            this.comments.Add(new Comment(content, userId));
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

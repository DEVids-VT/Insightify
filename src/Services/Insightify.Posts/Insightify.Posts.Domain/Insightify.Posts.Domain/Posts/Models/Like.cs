namespace Insightify.Posts.Domain.Posts.Models
{
    using Insightify.Posts.Domain.Common.Models;
    using Insightify.Posts.Domain.Posts.Exceptions;

    public class Like : Entity<Guid>
    {
        internal Like(string userId, DateTime timeStamp)
        {
            this.UserId = userId;
            this.Timestamp = timeStamp;
        }
        public string UserId { get; }
        public DateTime Timestamp { get; }
    }
}

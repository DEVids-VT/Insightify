namespace Insightify.Posts.Domain.Posts.Models
{
    using Insightify.Posts.Domain.Common.Models;

    public class Save : Entity<Guid>
    {
        internal Save(string userId, DateTime timeStamp)
        {
            this.UserId = userId;
            this.Timestamp = timeStamp;
        }
        public string UserId { get; }

        public DateTime Timestamp { get; }
    }
}

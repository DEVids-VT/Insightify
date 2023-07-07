namespace Insightify.Posts.Domain.Posts.Models
{
    using Insightify.Posts.Domain.Common.Models;

    public class Like : Entity<int>
    {
        //EFCore bug
        private Like() {}
        
        internal Like(string userId, DateTime timeStamp)
        {
            this.UserId = userId;
            this.Timestamp = timeStamp;
        }
        public string UserId { get; private set; }
        public DateTime Timestamp { get; private set; }
    }
}

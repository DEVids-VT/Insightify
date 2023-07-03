namespace Insightify.Posts.Domain.Posts.Models
{
    using Insightify.Posts.Domain.Common.Models;

    public class Save : Entity<int>
    {
        //EFCore bug
        private Save() {}

        internal Save(string userId, DateTime timeStamp)
        {
            this.UserId = userId;
            this.Timestamp = timeStamp;
        }
        public string UserId { get; private set; }

        public DateTime Timestamp { get; private set; }
    }
}

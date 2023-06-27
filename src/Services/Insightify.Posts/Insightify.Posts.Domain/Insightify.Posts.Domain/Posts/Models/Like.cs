using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Domain.Common.Models;
using Insightify.Posts.Domain.Posts.Exceptions;

namespace Insightify.Posts.Domain.Posts.Models
{
    public class Like : ValueObject
    {
        internal Like(string userId, DateTime timeStamp)
        {
            this.Validate(userId);
            this.UserId = userId;
            this.Timestamp = timeStamp;
        }
        public string UserId { get; private set; }

        public DateTime Timestamp { get; private set; }

        private void Validate(string userId)
            => Guard.AgainstEmptyString<InvalidLikeException>(userId);

    }
}

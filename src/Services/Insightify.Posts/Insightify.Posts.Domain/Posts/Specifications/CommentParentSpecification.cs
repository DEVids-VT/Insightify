using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Domain.Common;
using Insightify.Posts.Domain.Posts.Models;

namespace Insightify.Posts.Domain.Posts.Specifications
{
    public class CommentParentSpecification : Specification<Post>
    {
        private readonly int? postId;
        private readonly int? commentId;

        public CommentParentSpecification(int? postId, int? commentId)
        {
            this.postId = postId;
            this.commentId = commentId;
        }
        protected override bool Include => (postId != null && commentId == null) || (postId == null && commentId != null);
        public override Expression<Func<Post, bool>> ToExpression()
        {
            if (postId != null)
            {
                return post => post.Id == postId;
            }
            else
            {
                return post => post.Comments.Any(c => c.Id == commentId);
            }
        }
    }
}

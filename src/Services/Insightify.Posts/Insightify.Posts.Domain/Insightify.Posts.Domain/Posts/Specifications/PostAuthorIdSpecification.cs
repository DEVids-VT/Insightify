using Insightify.Posts.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Domain.Posts.Models;

namespace Insightify.Posts.Domain.Posts.Specifications
{
    public class PostAuthorIdSpecification : Specification<Post>
    {
        private readonly string? authorId;

        public PostAuthorIdSpecification(string? authorId) => this.authorId = authorId;

        protected override bool Include => this.authorId != null;
        public override Expression<Func<Post, bool>> ToExpression() => post => post.AuthorId == this.authorId!;
    }

}

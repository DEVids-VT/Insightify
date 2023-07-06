namespace Insightify.Posts.Domain.Posts.Specifications
{
    using Insightify.Posts.Domain.Common;
    using Insightify.Posts.Domain.Posts.Models;
    using System.Linq.Expressions;
    public class PostTitleSpecification : Specification<Post>
    {
        private readonly string? title;

        public PostTitleSpecification(string? title) => this.title = title;

        protected override bool Include => this.title != null;
        public override Expression<Func<Post, bool>> ToExpression() => post => post.Title.Contains(this.title!);
        
    }
}

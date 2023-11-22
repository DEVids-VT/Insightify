namespace Insightify.Posts.Application.Posts.Queries.Comments
{
    using Insightify.Posts.Application.Common.Mapping;
    using Insightify.Posts.Domain.Posts.Models;
    public class CommentOutputModel : IMapFrom<Comment>
    {
        public int Id { get; set; }
        public string AuthorId { get; set; } = default!;
        public string Content { get; set; } = default!;
    }
}

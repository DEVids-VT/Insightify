
namespace Insightify.Posts.Application.Posts.Queries.Likes
{
    using Insightify.Posts.Application.Common.Mapping;
    using Insightify.Posts.Domain.Posts.Models;
    public class LikeOutputModel : IMapFrom<Like>
    {
        public string UserId { get; set; } = default!;
        public DateTime Timestamp { get; set; }
    }
}

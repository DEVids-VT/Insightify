namespace Insightify.Posts.Application.Posts.Queries.Saves
{
    using Insightify.Posts.Application.Common.Mapping;
    using Insightify.Posts.Domain.Posts.Models;
    public class SaveOutputModel : IMapFrom<Save>
    {
        public string UserId { get; set; } = default!;
        public DateTime Timestamp { get; set; }
    }
}

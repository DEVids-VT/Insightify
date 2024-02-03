using Insightify.Web.Gateway.Clients.Models;
using Insightify.Web.Gateway.Infrastructure.Mapping;

namespace Insightify.Web.Gateway.Models
{
    public class PostOutputModel : IMapFrom<PostResponseModel>
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string AuthorId { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public int LikeCount { get; set; }
        public int SaveCount { get; set; }
        public int CommentCount { get; set; }
        public DateTime UploadDate { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}

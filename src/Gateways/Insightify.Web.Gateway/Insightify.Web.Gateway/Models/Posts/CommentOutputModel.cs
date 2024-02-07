using Insightify.Web.Gateway.Clients.Models.Posts;
using Insightify.Web.Gateway.Infrastructure.Mapping;

namespace Insightify.Web.Gateway.Models.Posts
{
    public class CommentOutputModel : IMapFrom<CommentResponseModel>
    {
        public int Id { get; set; }
        public string Content { get; set; } = default!;
        public string AuthorId { get; set; } = default!;
    }
}

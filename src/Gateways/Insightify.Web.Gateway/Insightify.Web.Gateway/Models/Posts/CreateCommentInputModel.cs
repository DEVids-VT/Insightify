using Insightify.Web.Gateway.Infrastructure.Mapping;

namespace Insightify.Web.Gateway.Models.Posts
{
    public class CreateCommentInputModel
    {
        public int PostId { get; set; }
        public string Content { get; set; } = default!;
    }
}

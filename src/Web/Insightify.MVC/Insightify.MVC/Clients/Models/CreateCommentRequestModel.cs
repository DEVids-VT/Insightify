using Insightify.MVC.Infrastructure.Mapping;
using Insightify.MVC.Models.Posts;

namespace Insightify.MVC.Clients.Models
{
    public class CreateCommentRequestModel : IMapFrom<CreateCommentInputModel>
    {
        public int PostId { get; set; }
        public string Content { get; set; } = default!;
    }
}

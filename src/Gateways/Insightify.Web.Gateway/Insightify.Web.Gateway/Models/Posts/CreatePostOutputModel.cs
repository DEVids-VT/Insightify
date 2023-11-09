using Insightify.Web.Gateway.Clients.Models.Posts;
using Insightify.Web.Gateway.Infrastructure.Mapping;

namespace Insightify.Web.Gateway.Models.Posts
{
    public class CreatePostOutputModel : IMapFrom<CreatePostResponseModel>
    {
        public int PostId { get; set; }
    }
}

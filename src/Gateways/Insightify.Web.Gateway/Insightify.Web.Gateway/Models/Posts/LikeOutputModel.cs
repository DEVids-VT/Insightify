using Insightify.Web.Gateway.Clients.Models.Posts;
using Insightify.Web.Gateway.Infrastructure.Mapping;

namespace Insightify.Web.Gateway.Models.Posts
{
    public class LikeOutputModel : IMapFrom<LikeResponseModel>
    {
        public string UserId { get; set; } = default!;
        public DateTime Timestamp { get; set; } = default!;
    }
}

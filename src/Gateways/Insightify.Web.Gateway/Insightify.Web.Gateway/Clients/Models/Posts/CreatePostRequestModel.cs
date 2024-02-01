using Insightify.Web.Gateway.Infrastructure.Mapping;
using Insightify.Web.Gateway.Models;

namespace Insightify.Web.Gateway.Clients.Models.Posts
{
    public class CreatePostRequestModel : IMapFrom<CreatePostInputModel>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public IEnumerable<string> Tags { get; set; } = new HashSet<string>();
    }
}

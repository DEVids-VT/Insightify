using Insightify.MVC.Clients.Models;
using Insightify.MVC.Infrastructure.Mapping;

namespace Insightify.MVC.Models.Posts
{
    public class LikeViewModel : IMapFrom<LikeResponseModel>
    {
        public string UserId { get; set; } = default!;
        public DateTime Timestamp { get; set; } = default!;
    }
}

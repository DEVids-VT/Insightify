using Insightify.MVC.Clients.Models;
using Insightify.MVC.Infrastructure.Mapping;

namespace Insightify.MVC.Models.Posts
{
    public class CommentViewModel : IMapFrom<CommentResponseModel>
    {
        public int Id { get; set; }
        public string Content { get; set; } = default!;
        public string AuthorId { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string UserPfp { get; set; } = default!;
    }
}

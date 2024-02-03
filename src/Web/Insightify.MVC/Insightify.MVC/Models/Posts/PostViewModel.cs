using Insightify.MVC.Clients.Models;
using Insightify.MVC.Infrastructure.Mapping;

namespace Insightify.MVC.Models.Posts
{
    public class PostViewModel : IMapFrom<PostsResponseModel>
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string AuthorId { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public int LikeCount { get; set; }
        public int SaveCount { get; set; }
        public int CommentCount { get; set; }
        public string? Username { get; set; }
        public string? UserImg { get; set; }
        public DateTime UploadDate { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}

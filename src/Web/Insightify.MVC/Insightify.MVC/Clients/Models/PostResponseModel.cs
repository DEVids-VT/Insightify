namespace Insightify.MVC.Clients.Models
{
    public class PostResponseModel
    { 
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string AuthorId { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public int LikeCount { get; set; }
        public int SaveCount { get; set; }
        public int CommentCount { get; set; }
    }
}

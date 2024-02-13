namespace Insightify.MVC.Clients.Models
{
    public class CommentResponseModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = default!;
        public string AuthorId { get; set; } = default!;
    }
}

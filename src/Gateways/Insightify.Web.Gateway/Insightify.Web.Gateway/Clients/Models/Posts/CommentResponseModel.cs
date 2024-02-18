namespace Insightify.Web.Gateway.Clients.Models.Posts
{
    public class CommentResponseModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; } = default!;
        public string Content { get; set; } = default!;
    }
}

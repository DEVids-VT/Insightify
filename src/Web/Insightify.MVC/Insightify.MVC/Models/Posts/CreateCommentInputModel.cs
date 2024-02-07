namespace Insightify.MVC.Models.Posts
{
    public class CreateCommentInputModel
    {
        public int PostId { get; set; }
        public string Content { get; set; } = default!;
    }
}

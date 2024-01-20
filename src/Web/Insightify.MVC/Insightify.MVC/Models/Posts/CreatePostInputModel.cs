namespace Insightify.MVC.Models.Posts
{
    public class CreatePostInputModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}

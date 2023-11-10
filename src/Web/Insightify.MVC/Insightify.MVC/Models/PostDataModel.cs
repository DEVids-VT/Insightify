namespace Insightify.MVC.Models
{
    public class PostDataModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}

namespace Insightify.Web.Gateway.Models
{
    public class CreatePostInputModel
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public IEnumerable<string> Tags { get; set; } = new HashSet<string>();
    }
}
using Insightify.MVC.Clients.Models;
using Insightify.MVC.Infrastructure.Mapping;

namespace Insightify.MVC.Models
{
    public class NewsViewModel : IMapFrom<NewsResponceModel>
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Source { get; set; }
        public string Country { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Id { get; set; } = default!;
        public int RowVersion { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}

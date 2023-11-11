using AutoMapper;
using Insightify.Web.Gateway.Clients.Models;
using Insightify.Web.Gateway.Infrastructure.Mapping;

namespace Insightify.Web.Gateway.Models
{
    public class NewsArticleOutputModel : IMapFrom<NewsArticleResponseModel>
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

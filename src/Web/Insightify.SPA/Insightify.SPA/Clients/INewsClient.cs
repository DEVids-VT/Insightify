using Insightify.SPA.Models;
using Refit;

namespace Insightify.SPA.Clients
{
    public interface INewsClient
    {
        [Get("/news")]
        Task<IApiResponse<IEnumerable<NewsArticle>>> Articles([Query] int pageIndex = 1, [Query] int pageSize = 50);
    }
}

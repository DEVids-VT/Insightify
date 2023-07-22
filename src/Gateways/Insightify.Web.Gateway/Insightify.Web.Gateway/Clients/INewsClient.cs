using Insightify.Web.Gateway.Clients.Models;
using Refit;

namespace Insightify.Web.Gateway.Clients
{
    public interface INewsClient
    {
        [Get("/news/articles")]
        Task<ApiResponse<List<NewsArticleResponseModel>>> Articles([Query] int pageIndex = 1, [Query] int pageSize = 50);
    }
}

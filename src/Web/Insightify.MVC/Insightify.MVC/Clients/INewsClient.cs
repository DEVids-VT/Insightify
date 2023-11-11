using Insightify.MVC.Clients.Models;
using Refit;

namespace Insightify.MVC.Clients
{
    public interface INewsClient
    {
        [Get("/news")]
        Task<ApiResponse<List<NewsResponseModel>>> News([Query] string? title = null, [Query] int pageIndex = 1, [Query] int pageSize = 50);
    }
}

using Insightify.MVC.Clients.Models;
using Refit;

namespace Insightify.MVC.Clients
{
    public interface IPostsClient
    {
        [Get("/post/all")]
        Task<ApiResponse<List<PostResponseModel>>> Posts([Query] string? title = null, [Query] int pageIndex = 1, [Query] int pageSize = 50);
    }
}

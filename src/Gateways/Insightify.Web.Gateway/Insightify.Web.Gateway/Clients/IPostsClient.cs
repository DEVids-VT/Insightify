using Insightify.Web.Gateway.Clients.Models;
using Insightify.Web.Gateway.Models;
using Refit;

namespace Insightify.Web.Gateway.Clients
{
    public interface IPostsClient
    {
        [Get("/posts")]
        Task<ApiResponse<List<PostResponseModel>>> Posts([Query] string? title = null, [Query] int pageIndex = 1, [Query] int pageSize = 50);
    }
}

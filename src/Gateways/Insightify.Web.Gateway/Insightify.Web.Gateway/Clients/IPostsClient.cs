using Insightify.Web.Gateway.Clients.Models;
using Insightify.Web.Gateway.Clients.Models.Posts;
using Insightify.Web.Gateway.Models;
using Refit;

namespace Insightify.Web.Gateway.Clients
{
    public interface IPostsClient
    {
        [Get("/posts")]
        Task<ApiResponse<List<PostResponseModel>>> Posts([Query] string? title = null, [Query] int pageIndex = 1, [Query] int pageSize = 50);

        [Post("/posts/create")]
        Task<ApiResponse<CreatePostResponseModel>> Create([Body] CreatePostRequestModel post);

    }
}

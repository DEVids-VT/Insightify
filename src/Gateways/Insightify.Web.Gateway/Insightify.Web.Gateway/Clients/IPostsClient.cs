using Insightify.Web.Gateway.Clients.Models;
using Insightify.Web.Gateway.Clients.Models.Posts;
using Insightify.Web.Gateway.Models;
using Refit;

namespace Insightify.Web.Gateway.Clients
{
    public interface IPostsClient
    {
        [Get("/posts")]
        Task<ApiResponse<List<PostResponseModel>>> Posts([Query] string? title = null, [Query] int page = 1, [Query] int pageSize = 50);

        [Get("/posts/{postId}")]
        Task<ApiResponse<PostResponseModel>> Post(int postId);

        [Post("/posts/create")]
        Task<ApiResponse<CreatePostResponseModel>> Create([Body] CreatePostRequestModel post);

        [Get("/posts/{postId}/likes")]
        Task<ApiResponse<List<LikeResponseModel>>> Likes(int postId);

        [Post("/posts/{postId}/like")]
        Task Like(int postId);
        [Post("/posts/{postId}/dislike")]
        Task Dislike(int postId);
    }
}

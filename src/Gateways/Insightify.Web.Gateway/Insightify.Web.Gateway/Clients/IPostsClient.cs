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

        [Post("/posts/create")]
        Task<ApiResponse<CreatePostResponseModel>> Create([Body] CreatePostRequestModel post);

        [Post("/posts/{postId}/like")]
        Task Like(int postId);
        [Post("/posts/{postId}/dislike")]
        Task Dislike(int postId);

        [Post("/posts/{postId}/save")]
        Task Save(int postId);
        [Post("/posts/{postId}/unsave")]
        Task Unsave(int postId);

        //[Post("/posts/comment")]
        //Task Comment([Body] int id, [Body] string content);

        //[Post("/posts/uncomment")]
        //Task RemoveComment([Body] int id, [Body] int postId);

    }
}

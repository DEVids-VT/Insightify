using Insightify.MVC.Clients.Models;
using Insightify.MVC.Models.Posts;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Insightify.MVC.Clients
{
    public interface IPostsClient
    {
        [Get("/post/all")]
        Task<ApiResponse<List<PostsResponseModel>>> Posts([Query] string? title = null, [Query] int pageIndex = 1, [Query] int pageSize = 50);

        [Post("/post/create")]
        Task<ApiResponse<CreatePostResponseModel>> Create([FromBody] PostModel post);
    }
}

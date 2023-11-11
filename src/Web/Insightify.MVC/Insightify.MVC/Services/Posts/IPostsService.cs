using Insightify.Framework.Pagination.Abstractions;
using Insightify.MVC.Clients.Models;
using Insightify.MVC.Models.Posts;

namespace Insightify.MVC.Services.Posts
{
    public interface IPostsService
    {
        Task<IPage<PostViewModel>> GetPosts(string? title = null, int pageIndex = 1, int pageSize = 50);
        Task<CreatePostResponceModel> CreatePost(CreatePostInputModel model);
    }
}

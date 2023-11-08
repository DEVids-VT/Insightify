using Insightify.Framework.Pagination.Abstractions;
using Insightify.MVC.Models;

namespace Insightify.MVC.Services.Posts
{
    public interface IPostsService
    {
        Task<IPage<PostViewModel>> GetPosts(string? title = null, int pageIndex = 1, int pageSize = 50);
    }
}

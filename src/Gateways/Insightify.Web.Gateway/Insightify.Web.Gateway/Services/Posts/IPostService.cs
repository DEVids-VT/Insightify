using Insightify.Framework.Pagination.Abstractions;
using Insightify.Web.Gateway.Models;
using Insightify.Web.Gateway.Models.Posts;

namespace Insightify.Web.Gateway.Services.Posts
{
    public interface IPostService
    {
        Task<IPage<PostOutputModel>> GetPosts(string? title = null, int pageIndex = 1, int pageSize = 50);
        Task<CreatePostOutputModel> CreatePost(CreatePostInputModel post);
    }
}

using Insightify.Framework.Pagination.Abstractions;
using Insightify.Web.Gateway.Models;
using Insightify.Web.Gateway.Models.Posts;

namespace Insightify.Web.Gateway.Services.Posts
{
    public interface IPostService
    {
        Task<IPage<PostOutputModel>> GetPosts(string? title = null, int pageIndex = 1, int pageSize = 50);
        Task<CreatePostOutputModel> CreatePost(CreatePostInputModel post);
        Task LikePost(int postId);
        Task DislikePost(int postId);
        Task SavePost(int postId);
        Task UnsavePost(int postId);

        //Task CommentOnPost(int postId, string content);
        //Task RemoveCommentOnPost(int commentId, int postId);

    }
}

using Insightify.Framework.Pagination.Abstractions;
using Insightify.MVC.Clients.Models;
using Insightify.MVC.Models.Posts;

namespace Insightify.MVC.Services.Posts
{
    public interface IPostsService
    {
        Task<IPage<PostViewModel>> GetPosts(string? title = null, int pageIndex = 1, int pageSize = 50);
        Task<CreatePostResponseModel> CreatePost(CreatePostInputModel model);
        Task<int> LikePost(int postId);
        Task<IEnumerable<LikeViewModel>> Likes(int postId);
        Task<PostViewModel> GetPost(int postId);
        Task Comment(CreateCommentInputModel comment);
        Task<IEnumerable<CommentViewModel>> Comments(int postId);
    }
}

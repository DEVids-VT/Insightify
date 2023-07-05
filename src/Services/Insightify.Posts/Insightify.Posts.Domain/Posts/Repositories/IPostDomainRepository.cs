namespace Insightify.Posts.Domain.Posts.Repositories
{
    using Insightify.Posts.Domain.Common;
    using Insightify.Posts.Domain.Posts.Models;

    public interface IPostDomainRepository : IDomainRepository<Post>
    {
        Task<Post?> Find(int id, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        bool UserHasPost(string userId, int postId);
        bool UserHasLiked(string userId, int postId);
        Task<int> FindLikeId(string userId, int postId, CancellationToken cancellationToken = default);
        bool UserHasSaved(string userId, int postId);
        Task<int> FindSaveId(string userId, int postId, CancellationToken cancellationToken = default);
    }
}

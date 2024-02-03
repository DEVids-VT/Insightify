namespace Insightify.Posts.Domain.Posts.Factories
{
    using Insightify.Posts.Domain.Common;
    using Insightify.Posts.Domain.Posts.Models;

    public interface IPostFactory : IFactory<Post>
    {
        IPostFactory WithTitle(string title);
        IPostFactory WithDescription(string description);
        IPostFactory WithAuthor(string authorId);
        IPostFactory WithImageUrl(string url);
        IPostFactory WithTags(IEnumerable<Tag> tags);
    }
}

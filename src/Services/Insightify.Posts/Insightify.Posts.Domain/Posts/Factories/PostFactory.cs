namespace Insightify.Posts.Domain.Posts.Factories
{
    using Insightify.Posts.Domain.Posts.Exceptions;
    using Insightify.Posts.Domain.Posts.Models;
    public class PostFactory : IPostFactory
    {
        private string title = default!;
        private string description = default!;
        private string authorId = default!;
        private string? imageUrl;

        private bool titleSet = false;
        private bool descriptionSet = false;
        private bool authorSet = false;

        public Post Build()
        {
            if (Equals(!this.titleSet || !this.descriptionSet || !this.authorSet))
            {
                throw new InvalidPostException("Title, description and authorId must have a value.");
            }

            return new Post(
                this.title,
                this.description,
                this.authorId,
                this.imageUrl);
        }

        public IPostFactory WithTitle(string title)
        {
            this.title = title;
            this.titleSet = true;
            return this;
        }

        public IPostFactory WithDescription(string description)
        {
            this.description = description;
            this.descriptionSet = true;
            return this;
        }

        public IPostFactory WithAuthor(string authorId)
        {
            this.authorId = authorId;
            this.authorSet = true;
            return this;
        }

        public IPostFactory WithImageUrl(string url)
        {
            this.imageUrl = url;
            return this;
        }
    }
}

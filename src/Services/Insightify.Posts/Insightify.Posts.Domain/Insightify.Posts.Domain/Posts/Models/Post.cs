namespace Insightify.Posts.Domain.Posts.Models
{
    using Insightify.Posts.Domain.Common.Models;
    using Insightify.Posts.Domain.Posts.Exceptions;

    using static ModelConstants.Common;
    using static ModelConstants.Post;

    internal class Post : Entity<Guid>
    {
        private readonly HashSet<Like> likes;
        private readonly HashSet<Save> saves;
        internal Post(string title, string description, string authorId, string? imageUrl)
        {
            this.Validate(title,description,authorId);
            if (imageUrl != null)
            {
                this.ValidateImageUrl(imageUrl);
                this.ImageUrl = imageUrl;
            }
            this.Title = title;
            this.Description = description;
            this.AuthorId = authorId;

            this.likes = new HashSet<Like>();
            this.saves = new HashSet<Save>();
        }
        public string Title { get; private set; }
        public string? ImageUrl { get; private set; }
        public string AuthorId { get; private set; }
        public string Description { get; private set; }

        public IReadOnlyCollection<Like> Likes => likes.ToList().AsReadOnly();
        public IReadOnlyCollection<Save> Saves => saves.ToList().AsReadOnly();

        private void Validate(string title, string description, string authorId)
        {
            this.ValidateTitle(title);
            this.ValidateDescription(description);
            this.ValidateAuthor(authorId);
        }
        private void ValidateTitle(string title)
            => Guard.ForStringLength<InvalidPostException>(
                title, 
                MinTitleLength, 
                MaxTitleLength, 
                nameof(this.Title));

        private void ValidateDescription(string description)
            => Guard.ForStringLength<InvalidPostException>(
                description, 
                MinDescriptionLength, 
                MaxDescriptionLength, 
                nameof(this.Description));

        private void ValidateImageUrl(string imageUrl)
            => Guard.ForValidUrl<InvalidPostException>(
                imageUrl,
                nameof(this.ImageUrl));

        private void ValidateAuthor(string authorId)
            => Guard.AgainstEmptyString<InvalidLikeException>(
                authorId, 
                nameof(this.AuthorId));

    }
}

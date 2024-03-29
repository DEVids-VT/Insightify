﻿namespace Insightify.Posts.Domain.Posts.Models
{
    using Insightify.Posts.Domain.Common.Models;
    using Insightify.Posts.Domain.Posts.Exceptions;
    using Insightify.Posts.Domain.Common;

    using static ModelConstants.Common;
    using static ModelConstants.Post;

    public class Post : Entity<int>, IAggregateRoot
    {
        private readonly HashSet<Like> likes;
        private readonly HashSet<Save> saves;
        private readonly HashSet<Comment> comments;
        private HashSet<Tag> tags;

        //EFCore bug
        private Post() {}

        internal Post(string title, string description, string authorId, string? imageUrl, IEnumerable<Tag> tags)
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
            this.UploadDate = DateTime.UtcNow;

            this.likes = new HashSet<Like>();
            this.saves = new HashSet<Save>();
            this.comments = new HashSet<Comment>();
            this.tags = new HashSet<Tag>(tags);
        }

        public string Title { get; private set; }
        public string? ImageUrl { get; private set; }
        public string AuthorId { get; }
        public string Description { get; private set; }
        public DateTime UploadDate { get; }

        public IReadOnlyCollection<Like> Likes => likes.ToList().AsReadOnly();
        public IReadOnlyCollection<Save> Saves => saves.ToList().AsReadOnly();
        public IReadOnlyCollection<Comment> Comments => comments.ToList().AsReadOnly();
        public IReadOnlyCollection<Tag> Tags => tags.ToList().AsReadOnly();

        public int TotalLikes => likes.Count;
        public int TotalSaves => saves.Count;
        public int TotalComments => comments.Count;

        public Post AddComment(string content, string userId)
        {
            this.comments.Add(new Comment(content, userId));
            return this;
        }
        public Post AddLike(string userId, DateTime timestamp)
        {
            if (likes.All(l => l.UserId != userId))
            {
                likes.Add(new Like(userId, timestamp));
            }
            return this;
        }

        public Post UpdateTags(IEnumerable<Tag> tags)
        {
            this.tags = tags.ToHashSet();
            return this;
        }
        public Post AddTag(Tag tag)
        {
            if (tags.All(t => t.Id != tag.Id))
            {
                tags.Add(tag);
            }
            return this;
        }

        public Post RemoveTag(int tagId)
        {
            if (tags.Any(t => t.Id == tagId))
            {
                tags.RemoveWhere(t => t.Id == tagId);
            }

            return this;
        }
        public Post RemoveTag(Tag tag)
        {
            if (tags.All(t => t.Id != tag.Id))
            {
                tags.Add(tag);
            }
            return this;
        }
        public Post AddSave(string userId, DateTime timestamp)
        {
            if (saves.All(s => s.UserId != userId))
            {
                saves.Add(new Save(userId, timestamp));
            }
            return this;
        }

        public Post RemoveLike(int likeId)
        {
            if (likes.Any(l => l.Id == likeId))
            {
                likes.RemoveWhere(l => l.Id == likeId);
            }
            return this;
        }
        public Post RemoveSave(int saveId)
        {
            if (saves.Any(s => s.Id == saveId))
            {
                saves.RemoveWhere(s => s.Id == saveId);
            }
            return this;
        }
        public Post RemoveComment(int commentId)
        {
            if (comments.Any(c => c.Id == commentId))
            {
                comments.RemoveWhere(c => c.Id == commentId);
            }
            return this;
        }
        public Post UpdateTitle(string title)
        {
            this.ValidateTitle(title);
            this.Title = title;

            return this;
        }

        public Post UpdateDescription(string description)
        {
            this.ValidateDescription(description);
            this.Description = description;

            return this;
        }

        public Post UpdateImageUrl(string imageUrl)
        {
            this.ValidateImageUrl(imageUrl);
            this.ImageUrl = imageUrl;

            return this;
        }
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
            => Guard.AgainstEmptyString<InvalidPostException>(
                authorId, 
                nameof(this.AuthorId));

    }
}

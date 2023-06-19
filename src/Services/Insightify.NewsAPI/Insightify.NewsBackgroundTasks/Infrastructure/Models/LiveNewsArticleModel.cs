namespace Insightify.NewsBackgroundTasks.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;
    using Insightify.Framework.MongoDb.Abstractions;
    using Insightify.Framework.MongoDb.Abstractions.Attributes;
    using Insightify.NewsBackgroundTasks.Common;

    using MongoDB.Bson.Serialization.Attributes;

    [CollectionName("live-news-articles")]
    public class LiveNewsArticleModel : MongoEntity
    {
        [MaxLength(ValidationConstants.LiveNews.Validation.AuthorMaxLength)]
        public string Author { get; set; } = null!;

        [BsonRequired]
        [MinLength(ValidationConstants.LiveNews.Validation.TitleMinLength)]
        [MaxLength(ValidationConstants.LiveNews.Validation.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [BsonRequired]
        [MinLength(ValidationConstants.LiveNews.Validation.DescriptionMinLength)]
        [MaxLength(ValidationConstants.LiveNews.Validation.DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [BsonRequired]
        public string Url { get; set; } = null!;

        [BsonRequired]
        public string Source { get; set; } = null!;

        [BsonRequired]
        public string Image { get; set; } = null!;

        [BsonRequired]
        public string Country { get; set; } = null!;

        [BsonRequired]
        public DateTime? PublishedAt { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Insightify.Framework.MongoDb.Abstractions;
using Insightify.Framework.MongoDb.Abstractions.Attributes;
using Insightify.NewsAPI.Common;
using MongoDB.Bson.Serialization.Attributes;


namespace Insightify.NewsAPI.Infrastructure.Models
{
    [CollectionName("live-news-articles")]
    public class NewsArticleModel : MongoEntity
    {
        [MaxLength(ValidationConstants.LiveNews.Validation.AuthorMaxLength)]
        public string Author { get; set; }

        [MinLength(ValidationConstants.LiveNews.Validation.TitleMinLength)]
        [MaxLength(ValidationConstants.LiveNews.Validation.TitleMaxLength)]
        public string Title { get; set; }

        [MinLength(ValidationConstants.LiveNews.Validation.DescriptionMinLength)]
        [MaxLength(ValidationConstants.LiveNews.Validation.DescriptionMaxLength)]
        public string Description { get; set; }

        public string Url { get; set; }

        public string Source { get; set; }

        public string Image { get; set; }

        public string Country { get; set; }
        
        public DateTime? PublishedAt { get; set; }
    }
}

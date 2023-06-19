using Newtonsoft.Json;

namespace Insightify.NewsBackgroundTasks.ResponceModels.LiveNews
{
    public  record NewsArticleResponseModel
    {
        [JsonProperty("author")]
        public string Author { get; init; } = null!;

        [JsonProperty("title")]
        public string Title { get; init; } = null!;

        [JsonProperty("description")] 
        public string Description { get; init; } = null!;

        [JsonProperty("url")] 
        public string Url { get; init; } = null!;

        [JsonProperty("source")] 
        public string Source { get; init; } = null!;

        [JsonProperty("image")]
        public string Image { get; init; } = null!;

        [JsonProperty("category")] 
        public string Category { get; init; } = null!;

        [JsonProperty("language")] 
        public string Language { get; init; } = null!;

        [JsonProperty("country")]
        public string Country { get; init; } = null!;

        [JsonProperty("published_at")]
        public DateTime? PublishedAt { get; init; }
    }
}

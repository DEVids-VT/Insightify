using Newtonsoft.Json;

namespace Insightify.NewsBackgroundTasks.ResponceModels.LiveNews
{
    public record LiveNewsResponseModel
    {
        [JsonProperty("pagination")]
        public PaginationModel Pagination { get; init; } = null!;

        [JsonProperty("data")]
        public List<NewsArticleResponseModel> Data { get; init; } = null!;
    }
}

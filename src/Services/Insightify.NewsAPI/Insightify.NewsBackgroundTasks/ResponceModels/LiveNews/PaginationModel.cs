using Newtonsoft.Json;

namespace Insightify.NewsBackgroundTasks.ResponceModels.LiveNews
{
    public record PaginationModel
    {
        [JsonProperty("limit")]
        public int? Limit { get; init; }

        [JsonProperty("offset")]
        public int? Offset { get; init; }

        [JsonProperty("count")]
        public int? Count { get; init; }

        [JsonProperty("total")]
        public int? Total { get; init; }
    }
}

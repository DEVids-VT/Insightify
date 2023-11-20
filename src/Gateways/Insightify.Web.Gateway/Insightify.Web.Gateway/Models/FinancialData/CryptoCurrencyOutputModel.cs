using Insightify.Web.Gateway.Clients.Models.FinancialData;
using Insightify.Web.Gateway.Infrastructure.Mapping;
using Newtonsoft.Json;

namespace Insightify.Web.Gateway.Models.FinancialData
{
    public class CryptoCurrencyOutputModel : IMapFrom<CryptoCurrencyResponseModel>
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("asset_platform_id", NullValueHandling = NullValueHandling.Ignore)]
        public object AssetPlatformId { get; set; }

        [JsonProperty("block_time_in_minutes", NullValueHandling = NullValueHandling.Ignore)]
        public int? BlockTimeInMinutes { get; set; }

        [JsonProperty("hashing_algorithm", NullValueHandling = NullValueHandling.Ignore)]
        public string HashingAlgorithm { get; set; }

        [JsonProperty("categories", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Categories { get; set; }

        [JsonProperty("preview_listing", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PreviewListing { get; set; }

        [JsonProperty("public_notice", NullValueHandling = NullValueHandling.Ignore)]
        public object PublicNotice { get; set; }

        [JsonProperty("additional_notices", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> AdditionalNotices { get; set; }

        [JsonProperty("localization", NullValueHandling = NullValueHandling.Ignore)]
        public Localization Localization { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public Description Description { get; set; }

        [JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
        public Links Links { get; set; }

        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public Image Image { get; set; }

        [JsonProperty("country_origin", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryOrigin { get; set; }

        [JsonProperty("genesis_date", NullValueHandling = NullValueHandling.Ignore)]
        public string GenesisDate { get; set; }

        [JsonProperty("sentiment_votes_up_percentage", NullValueHandling = NullValueHandling.Ignore)]
        public double? SentimentVotesUpPercentage { get; set; }

        [JsonProperty("sentiment_votes_down_percentage", NullValueHandling = NullValueHandling.Ignore)]
        public double? SentimentVotesDownPercentage { get; set; }

        [JsonProperty("watchlist_portfolio_users", NullValueHandling = NullValueHandling.Ignore)]
        public int? WatchlistPortfolioUsers { get; set; }

        [JsonProperty("market_cap_rank", NullValueHandling = NullValueHandling.Ignore)]
        public int? MarketCapRank { get; set; }

        [JsonProperty("market_data", NullValueHandling = NullValueHandling.Ignore)]
        public MarketData MarketData { get; set; }

        [JsonProperty("community_data", NullValueHandling = NullValueHandling.Ignore)]
        public CommunityData CommunityData { get; set; }

        [JsonProperty("public_interest_stats", NullValueHandling = NullValueHandling.Ignore)]
        public PublicInterestStats PublicInterestStats { get; set; }

        [JsonProperty("status_updates", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> StatusUpdates { get; set; }

        [JsonProperty("last_updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdated { get; set; }
    }
}


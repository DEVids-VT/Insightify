using Insightify.MVC.Infrastructure.Mapping;
using Newtonsoft.Json;

namespace Insightify.Web.Gateway.Models.FinancialData
{
    public class CryptoCurrencyModel : IMapFrom<CryptoCurrencyOutputModel>
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
    public class TotalVolume
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public long? Usd { get; set; }
    }

    public class Ath
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public long? Usd { get; set; }
    }

    public class AthChangePercentage
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class AthDate
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Usd { get; set; }
    }

    public class Atl
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class AtlChangePercentage
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class AtlDate
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Usd { get; set; }
    }

    public class CommunityData
    {
        [JsonProperty("facebook_likes", NullValueHandling = NullValueHandling.Ignore)]
        public object FacebookLikes { get; set; }

        [JsonProperty("twitter_followers", NullValueHandling = NullValueHandling.Ignore)]
        public int? TwitterFollowers { get; set; }

        [JsonProperty("reddit_average_posts_48h", NullValueHandling = NullValueHandling.Ignore)]
        public double? RedditAveragePosts48h { get; set; }

        [JsonProperty("reddit_average_comments_48h", NullValueHandling = NullValueHandling.Ignore)]
        public double? RedditAverageComments48h { get; set; }

        [JsonProperty("reddit_subscribers", NullValueHandling = NullValueHandling.Ignore)]
        public int? RedditSubscribers { get; set; }

        [JsonProperty("reddit_accounts_active_48h", NullValueHandling = NullValueHandling.Ignore)]
        public int? RedditAccountsActive48h { get; set; }

        [JsonProperty("telegram_channel_user_count", NullValueHandling = NullValueHandling.Ignore)]
        public object TelegramChannelUserCount { get; set; }
    }

    public class CurrentPrice
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public long? Usd { get; set; }
    }

    public class Description
    {
        [JsonProperty("en", NullValueHandling = NullValueHandling.Ignore)]
        public string En { get; set; }
    }

    public class FullyDilutedValuation
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public long? Usd { get; set; }
    }

    public class High24h
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public long? Usd { get; set; }
    }

    public class Image
    {
        [JsonProperty("thumb", NullValueHandling = NullValueHandling.Ignore)]
        public string Thumb { get; set; }

        [JsonProperty("small", NullValueHandling = NullValueHandling.Ignore)]
        public string Small { get; set; }

        [JsonProperty("large", NullValueHandling = NullValueHandling.Ignore)]
        public string Large { get; set; }
    }

    public class Links
    {
        [JsonProperty("homepage", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Homepage { get; set; }

        [JsonProperty("blockchain_site", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> BlockchainSite { get; set; }

        [JsonProperty("official_forum_url", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> OfficialForumUrl { get; set; }

        [JsonProperty("chat_url", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ChatUrl { get; set; }

        [JsonProperty("announcement_url", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> AnnouncementUrl { get; set; }

        [JsonProperty("twitter_screen_name", NullValueHandling = NullValueHandling.Ignore)]
        public string TwitterScreenName { get; set; }

        [JsonProperty("facebook_username", NullValueHandling = NullValueHandling.Ignore)]
        public string FacebookUsername { get; set; }

        [JsonProperty("bitcointalk_thread_identifier", NullValueHandling = NullValueHandling.Ignore)]
        public object BitcointalkThreadIdentifier { get; set; }

        [JsonProperty("telegram_channel_identifier", NullValueHandling = NullValueHandling.Ignore)]
        public string TelegramChannelIdentifier { get; set; }

        [JsonProperty("subreddit_url", NullValueHandling = NullValueHandling.Ignore)]
        public string SubredditUrl { get; set; }

        [JsonProperty("repos_url", NullValueHandling = NullValueHandling.Ignore)]
        public ReposUrl ReposUrl { get; set; }
    }

    public class Localization
    {
        [JsonProperty("en", NullValueHandling = NullValueHandling.Ignore)]
        public string En { get; set; }
    }

    public class Low24h
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public long? Usd { get; set; }
    }

    public class MarketCap
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public long? Usd { get; set; }
    }

    public class MarketCapChange24hInCurrency
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public long? Usd { get; set; }
    }

    public class MarketCapChangePercentage24hInCurrency
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class MarketData
    {
        [JsonProperty("current_price", NullValueHandling = NullValueHandling.Ignore)]
        public CurrentPrice CurrentPrice { get; set; }

        [JsonProperty("ath", NullValueHandling = NullValueHandling.Ignore)]
        public Ath Ath { get; set; }

        [JsonProperty("ath_change_percentage", NullValueHandling = NullValueHandling.Ignore)]
        public AthChangePercentage AthChangePercentage { get; set; }

        [JsonProperty("ath_date", NullValueHandling = NullValueHandling.Ignore)]
        public AthDate AthDate { get; set; }

        [JsonProperty("atl", NullValueHandling = NullValueHandling.Ignore)]
        public Atl Atl { get; set; }

        [JsonProperty("atl_change_percentage", NullValueHandling = NullValueHandling.Ignore)]
        public AtlChangePercentage AtlChangePercentage { get; set; }

        [JsonProperty("atl_date", NullValueHandling = NullValueHandling.Ignore)]
        public AtlDate AtlDate { get; set; }

        [JsonProperty("market_cap", NullValueHandling = NullValueHandling.Ignore)]
        public MarketCap MarketCap { get; set; }

        [JsonProperty("fully_diluted_valuation", NullValueHandling = NullValueHandling.Ignore)]
        public FullyDilutedValuation FullyDilutedValuation { get; set; }

        [JsonProperty("total_volume", NullValueHandling = NullValueHandling.Ignore)]
        public TotalVolume TotalVolume { get; set; }

        [JsonProperty("high_24h", NullValueHandling = NullValueHandling.Ignore)]
        public High24h High24h { get; set; }

        [JsonProperty("low_24h", NullValueHandling = NullValueHandling.Ignore)]
        public Low24h Low24h { get; set; }

        [JsonProperty("price_change_24h", NullValueHandling = NullValueHandling.Ignore)]
        public double? PriceChange24h { get; set; }

        [JsonProperty("price_change_percentage_24h", NullValueHandling = NullValueHandling.Ignore)]
        public double? PriceChangePercentage24h { get; set; }

        [JsonProperty("price_change_percentage_7d", NullValueHandling = NullValueHandling.Ignore)]
        public double? PriceChangePercentage7d { get; set; }

        [JsonProperty("price_change_percentage_14d", NullValueHandling = NullValueHandling.Ignore)]
        public double? PriceChangePercentage14d { get; set; }

        [JsonProperty("price_change_percentage_30d", NullValueHandling = NullValueHandling.Ignore)]
        public double? PriceChangePercentage30d { get; set; }

        [JsonProperty("price_change_percentage_60d", NullValueHandling = NullValueHandling.Ignore)]
        public double? PriceChangePercentage60d { get; set; }

        [JsonProperty("price_change_percentage_200d", NullValueHandling = NullValueHandling.Ignore)]
        public double? PriceChangePercentage200d { get; set; }

        [JsonProperty("price_change_percentage_1y", NullValueHandling = NullValueHandling.Ignore)]
        public double? PriceChangePercentage1y { get; set; }

        [JsonProperty("market_cap_change_24h", NullValueHandling = NullValueHandling.Ignore)]
        public long? MarketCapChange24h { get; set; }

        [JsonProperty("market_cap_change_percentage_24h", NullValueHandling = NullValueHandling.Ignore)]
        public double? MarketCapChangePercentage24h { get; set; }

        [JsonProperty("price_change_24h_in_currency", NullValueHandling = NullValueHandling.Ignore)]
        public PriceChange24hInCurrency PriceChange24hInCurrency { get; set; }

        [JsonProperty("price_change_percentage_1h_in_currency", NullValueHandling = NullValueHandling.Ignore)]
        public PriceChangePercentage1hInCurrency PriceChangePercentage1hInCurrency { get; set; }

        [JsonProperty("price_change_percentage_24h_in_currency", NullValueHandling = NullValueHandling.Ignore)]
        public PriceChangePercentage24hInCurrency PriceChangePercentage24hInCurrency { get; set; }

        [JsonProperty("price_change_percentage_7d_in_currency", NullValueHandling = NullValueHandling.Ignore)]
        public PriceChangePercentage7dInCurrency PriceChangePercentage7dInCurrency { get; set; }

        [JsonProperty("price_change_percentage_14d_in_currency", NullValueHandling = NullValueHandling.Ignore)]
        public PriceChangePercentage14dInCurrency PriceChangePercentage14dInCurrency { get; set; }

        [JsonProperty("price_change_percentage_30d_in_currency", NullValueHandling = NullValueHandling.Ignore)]
        public PriceChangePercentage30dInCurrency PriceChangePercentage30dInCurrency { get; set; }

        [JsonProperty("price_change_percentage_60d_in_currency", NullValueHandling = NullValueHandling.Ignore)]
        public PriceChangePercentage60dInCurrency PriceChangePercentage60dInCurrency { get; set; }

        [JsonProperty("price_change_percentage_200d_in_currency", NullValueHandling = NullValueHandling.Ignore)]
        public PriceChangePercentage200dInCurrency PriceChangePercentage200dInCurrency { get; set; }

        [JsonProperty("price_change_percentage_1y_in_currency", NullValueHandling = NullValueHandling.Ignore)]
        public PriceChangePercentage1yInCurrency PriceChangePercentage1yInCurrency { get; set; }

        [JsonProperty("market_cap_change_24h_in_currency", NullValueHandling = NullValueHandling.Ignore)]
        public MarketCapChange24hInCurrency MarketCapChange24hInCurrency { get; set; }

        [JsonProperty("market_cap_change_percentage_24h_in_currency", NullValueHandling = NullValueHandling.Ignore)]
        public MarketCapChangePercentage24hInCurrency MarketCapChangePercentage24hInCurrency { get; set; }

        [JsonProperty("total_supply", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalSupply { get; set; }

        [JsonProperty("max_supply", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxSupply { get; set; }

        [JsonProperty("circulating_supply", NullValueHandling = NullValueHandling.Ignore)]
        public long? CirculatingSupply { get; set; }

        [JsonProperty("last_updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdated { get; set; }
    }

    public class PriceChange24hInCurrency
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class PriceChangePercentage14dInCurrency
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class PriceChangePercentage1hInCurrency
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class PriceChangePercentage1yInCurrency
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class PriceChangePercentage200dInCurrency
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class PriceChangePercentage24hInCurrency
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class PriceChangePercentage30dInCurrency
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class PriceChangePercentage60dInCurrency
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class PriceChangePercentage7dInCurrency
    {
        [JsonProperty("usd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Usd { get; set; }
    }

    public class PublicInterestStats
    {
        [JsonProperty("alexa_rank", NullValueHandling = NullValueHandling.Ignore)]
        public int? AlexaRank { get; set; }

        [JsonProperty("bing_matches", NullValueHandling = NullValueHandling.Ignore)]
        public object BingMatches { get; set; }
    }

    public class ReposUrl
    {
        [JsonProperty("github", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Github { get; set; }

        [JsonProperty("bitbucket", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Bitbucket { get; set; }
    }
}


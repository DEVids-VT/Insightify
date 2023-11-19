using Insightify.MVC.Models.FinancialData.MarketDataModels;

namespace Insightify.Web.Gateway.Models.FinancialData
{
    public class CryptoCurrencyOutputModel
    {
        public string Id { get; set; }
        public string Symbol { get; set; }

        public string Name { get; set; }

        public object AssetPlatformId { get; set; }

        public int? BlockTimeInMinutes { get; set; }

        public string HashingAlgorithm { get; set; }

        public List<string> Categories { get; set; }

        public bool? PreviewListing { get; set; }

        public object PublicNotice { get; set; }

        public List<object> AdditionalNotices { get; set; }

        public Localization Localization { get; set; }

        public Description Description { get; set; }

        public Links Links { get; set; }

        public Image Image { get; set; }

        public string CountryOrigin { get; set; }

        public string GenesisDate { get; set; }

        public double? SentimentVotesUpPercentage { get; set; }

        public double? SentimentVotesDownPercentage { get; set; }

        public int? WatchlistPortfolioUsers { get; set; }

        public int? MarketCapRank { get; set; }

        public MarketData MarketData { get; set; }

        public CommunityData CommunityData { get; set; }

        public PublicInterestStats PublicInterestStats { get; set; }

        public List<object> StatusUpdates { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}


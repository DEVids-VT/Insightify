namespace Insightify.MVC.Models.FinancialData.MarketDataModels
{
    public class MarketData
    {
        public CurrentPrice CurrentPrice { get; set; }

        public Ath Ath { get; set; }

        public AthChangePercentage AthChangePercentage { get; set; }

        public AthDate AthDate { get; set; }

        public Atl Atl { get; set; }

        public AtlChangePercentage AtlChangePercentage { get; set; }

        public AtlDate AtlDate { get; set; }

        public MarketCap MarketCap { get; set; }

        public FullyDilutedValuation FullyDilutedValuation { get; set; }

        public TotalVolume TotalVolume { get; set; }

        public High24h High24h { get; set; }

        public Low24h Low24h { get; set; }

        public double? PriceChange24h { get; set; }

        public double? PriceChangePercentage24h { get; set; }

        public double? PriceChangePercentage7d { get; set; }

        public double? PriceChangePercentage14d { get; set; }

        public double? PriceChangePercentage30d { get; set; }

        public double? PriceChangePercentage60d { get; set; }

        public double? PriceChangePercentage200d { get; set; }

        public double? PriceChangePercentage1y { get; set; }

        public long? MarketCapChange24h { get; set; }

        public double? MarketCapChangePercentage24h { get; set; }

        public PriceChange24hInCurrency PriceChange24hInCurrency { get; set; }

        public PriceChangePercentage1hInCurrency PriceChangePercentage1hInCurrency { get; set; }

        public PriceChangePercentage24hInCurrency PriceChangePercentage24hInCurrency { get; set; }

        public PriceChangePercentage7dInCurrency PriceChangePercentage7dInCurrency { get; set; }

        public PriceChangePercentage14dInCurrency PriceChangePercentage14dInCurrency { get; set; }

        public PriceChangePercentage30dInCurrency PriceChangePercentage30dInCurrency { get; set; }

        public PriceChangePercentage60dInCurrency PriceChangePercentage60dInCurrency { get; set; }

        public PriceChangePercentage200dInCurrency PriceChangePercentage200dInCurrency { get; set; }

        public PriceChangePercentage1yInCurrency PriceChangePercentage1yInCurrency { get; set; }

        public MarketCapChange24hInCurrency MarketCapChange24hInCurrency { get; set; }

        public MarketCapChangePercentage24hInCurrency MarketCapChangePercentage24hInCurrency { get; set; }

        public long? TotalSupply { get; set; }

        public long? MaxSupply { get; set; }

        public long? CirculatingSupply { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
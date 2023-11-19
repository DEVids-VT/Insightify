namespace Insightify.Web.Gateway.Models.FinancialData
{
    public class MarketChartOutputModel
    {
        public List<MarketValue> Prices { get; set; }
        public List<MarketValue> MarketCaps { get; set; }
        public List<MarketValue> TotalVolumes { get; set; }
    }
}

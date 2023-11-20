using Insightify.MVC.Infrastructure.Mapping;
using System.Text.Json.Serialization;

namespace Insightify.Web.Gateway.Models.FinancialData
{
    public class MarketChartModel : IMapFrom<MarketChartOutputModel>
    {
        [JsonPropertyName("prices")]
        public List<MarketValue> Prices { get; set; }
        [JsonPropertyName("market_caps")]
        public List<MarketValue> MarketCaps { get; set; }
        [JsonPropertyName("total_volumes")]

        public List<MarketValue> TotalVolumes { get; set; }
    }
    public class MarketValue
    {
        public long Timestamp { get; set; }
        public decimal Value { get; set; }
    }
}

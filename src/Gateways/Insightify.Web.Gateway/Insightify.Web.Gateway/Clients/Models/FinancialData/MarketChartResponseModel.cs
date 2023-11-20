using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Insightify.Web.Gateway.Clients.Models.FinancialData
{
    public class MarketChartResponseModel
    {
        [JsonProperty("prices")]
        public List<MarketValue> Prices { get; set; }
        [JsonProperty("market_caps")]
        public List<MarketValue> MarketCaps { get; set; }
        [JsonProperty("total_volumes")]

        public List<MarketValue> TotalVolumes { get; set; }
    }
    public class MarketValue
    {
        public long Timestamp { get; set; }
        public decimal Value { get; set; }
    }
}

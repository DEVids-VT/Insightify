using Insightify.Web.Gateway.Clients.Models.FinancialData;
using Insightify.Web.Gateway.Infrastructure.Mapping;
using Newtonsoft.Json;

namespace Insightify.Web.Gateway.Models.FinancialData
{
    public class MarketChartOutputModel : IMapFrom<MarketChartResponseModel>
    {
        [JsonProperty("prices")]

        public List<MarketValue> Prices { get; set; }
        [JsonProperty("market_caps")]
        public List<MarketValue> MarketCaps { get; set; }
        [JsonProperty("total_volumes")]

        public List<MarketValue> TotalVolumes { get; set; }
    }
}

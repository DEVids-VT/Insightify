using Newtonsoft.Json;

namespace Insighify.FinancialDataApi.ResponceModels.Crypto
{
    public class CoinHistory
    {
        [JsonProperty("prices")]
        public List<List<double?>> Prices { get; set; }

        [JsonProperty("market_caps")]
        public List<List<double?>> MarketCaps { get; set; }

        [JsonProperty("total_volumes")]
        public List<List<double?>> TotalVolumes { get; set; }
    }
}

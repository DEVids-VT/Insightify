using Newtonsoft.Json;

namespace Insighify.FinancialDataApi.ResponceModels.Crypto
{
    public class CoinInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

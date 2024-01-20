using Insightify.FinancialBackgroundTasks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Insightify.FinancialBackgroundTasks.Models
{
    public class MarketChartModel
    {
        [JsonProperty("prices")]

        public List<MarketValue> Prices { get; set; }
        [JsonProperty("market_caps")]
        public List<MarketValue> MarketCaps { get; set; }
        [JsonProperty("total_volumes")]

        public List<MarketValue> TotalVolumes { get; set; }
    }

    [JsonConverter(typeof(CryptoDataModelConverter))]
    public class MarketValue
    {
        public long Timestamp { get; set; }
        public decimal Value { get; set; }
    }
}

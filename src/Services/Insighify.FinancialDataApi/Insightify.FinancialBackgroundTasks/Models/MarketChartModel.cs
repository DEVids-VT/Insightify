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
        public IEnumerable<CryptoDataModel> Prices { get; set; }
    }

    [JsonConverter(typeof(CryptoDataModelConverter))]
    public class CryptoDataModel
    {
        public ulong MarketCap { get; set; }
        public decimal Price { get; set; }
    }
}

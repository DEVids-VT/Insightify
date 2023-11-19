using Insightify.MVC.Infrastructure.Mapping;
using Insightify.MVC.Models.FinancialData.MarketDataModels;
using Newtonsoft.Json;

namespace Insightify.Web.Gateway.Models.FinancialData
{
    public class MarketChartModel : IMapFrom<MarketChartOutputModel>
    {
        public List<MarketValue> Prices { get; set; }

        public List<MarketValue> MarketCaps { get; set; }

        public List<MarketValue> TotalVolumes { get; set; }
    }
}

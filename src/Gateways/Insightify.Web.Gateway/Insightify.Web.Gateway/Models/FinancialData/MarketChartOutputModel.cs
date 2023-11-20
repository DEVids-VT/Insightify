using Insightify.Web.Gateway.Clients.Models.FinancialData;
using Insightify.Web.Gateway.Infrastructure.Mapping;
using Newtonsoft.Json;

namespace Insightify.Web.Gateway.Models.FinancialData
{
    public class MarketChartOutputModel : IMapFrom<MarketChartResponseModel>
    {

        public List<MarketValue> Prices { get; set; }
        public List<MarketValue> MarketCaps { get; set; }
        public List<MarketValue> TotalVolumes { get; set; }
    }
}

using Insightify.Web.Gateway.Models.FinancialData;

namespace Insightify.MVC.Models.FinancialData
{
    public class DashboardModel
    {
        public MarketChartModel ChartData { get; set; }
        public List<DashboardCurrencyModel> Currencies { get; set; }
    }
}

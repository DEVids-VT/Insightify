using Insightify.Web.Gateway.Models.FinancialData;

namespace Insightify.MVC.Models.FinancialData
{
    public class DashboardModel
    {
        public MarketChartModel ChartsData { get; set; }
        public CryptoCurrencyModel Currency { get; set; }
    }
}

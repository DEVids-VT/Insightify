using Insightify.Framework.Pagination.Abstractions;
using Insightify.MVC.Models;
using Insightify.MVC.Models.FinancialData;
using Insightify.Web.Gateway.Models.FinancialData;

namespace Insightify.MVC.Services.FinancialData
{
    public interface IFinancialDataService
    {
        Task<IPage<CryptoCurrencyModel>> GetAllCurrencies(string? title = null, int pageIndex = 1, int pageSize = 50);
        Task<CryptoCurrencyModel> Currency(string currency);
        Task<MarketChartModel> Chart(string currency);
        Task<DashboardModel> Dashboard();
    }
}

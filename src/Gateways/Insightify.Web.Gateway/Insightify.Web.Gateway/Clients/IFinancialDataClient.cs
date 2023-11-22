using Insightify.Web.Gateway.Clients.Models;
using Insightify.Web.Gateway.Clients.Models.FinancialData;
using Refit;

namespace Insightify.Web.Gateway.Clients
{
    public interface IFinancialDataClient
    {
        [Get("/{currency}")]
        Task<ApiResponse<CryptoCurrencyResponseModel>> GetCurrency(string currency);
        [Get("/{currency}/chart")]
        Task<ApiResponse<MarketChartResponseModel>> GetMarketChart(string currency);
        [Get("/all")]
        Task<ApiResponse<List<CryptoCurrencyResponseModel>>> GetAllCurrencies();
    }
}

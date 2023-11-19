using Insightify.Web.Gateway.Models.FinancialData;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Insightify.MVC.Clients
{
    public interface IFinancialDataClient
    {
        [Get("/all")]
        Task<ApiResponse<List<CryptoCurrencyModel>>> AllCurrencies(string? title = null, int pageIndex = 1, int pageSize = 50);

        [Get("/{currency}")]
        Task<ApiResponse<CryptoCurrencyModel>> Currency([FromRoute] string currency);

        [Get("/{currency}/chart")]
        Task<ApiResponse<MarketChartModel>> Chart([FromRoute] string currency);
    }
}

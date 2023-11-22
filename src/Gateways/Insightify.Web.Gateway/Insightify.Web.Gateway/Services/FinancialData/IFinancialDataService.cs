using Insightify.Web.Gateway.Infrastructure.Enums;
using Insightify.Web.Gateway.Models.FinancialData;

namespace Insightify.Web.Gateway.Services.FinancialData
{
    public interface IFinancialDataService
    {
        Task<IEnumerable<CryptoCurrencyOutputModel>> GetAllCurrencies();
        Task<CryptoCurrencyOutputModel> GetCurrency(CryptoCurrency currency);
        Task<MarketChartOutputModel> GetMarketChart(CryptoCurrency currency);
    }
}

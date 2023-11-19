using Insighify.FinancialDataApi.Infrastructure.Enums;
using Insighify.FinancialDataApi.Models;

namespace Insighify.FinancialDataApi.Services.Contracts
{
    public interface ICryptoDataService
    {
        Task<CryptoCurrencyModel> GetCryptoCurrencyAsync(CryptoCurrency currency);
        Task<IEnumerable<CryptoCurrencyModel>> GetAllCurrenciesAsync();
        Task<MarketChartModel> GetMarketChartAsync(CryptoCurrency currency);
    }
}

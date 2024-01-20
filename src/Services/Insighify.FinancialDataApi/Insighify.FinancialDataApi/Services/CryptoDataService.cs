using Insighify.FinancialDataApi.Infrastructure.Enums;
using Insighify.FinancialDataApi.Models;
using Insighify.FinancialDataApi.Services.Contracts;
using Newtonsoft.Json;
using NReJSON;
using StackExchange.Redis;

namespace Insighify.FinancialDataApi.Services
{
    public class CryptoDataService  : ICryptoDataService
    {
        private readonly IDatabase _redis;
        private readonly ILogger _logger;

        public CryptoDataService(IDatabase redis, ILogger<CryptoDataService> logger)
        {
            _redis = redis;
            _logger = logger;
        }
        public async Task<CryptoCurrencyModel> GetCryptoCurrencyAsync(CryptoCurrency currency)
        {
            var cryptoCurrency = await _redis.JsonGetAsync(currency.ToString().ToLower());
            return JsonConvert.DeserializeObject<CryptoCurrencyModel>(cryptoCurrency.ToString());
        }

        public async Task<IEnumerable<CryptoCurrencyModel>> GetAllCurrenciesAsync()
        {
            var currencies = new List<CryptoCurrencyModel>();
            foreach (CryptoCurrency currency in Enum.GetValues(typeof(CryptoCurrency)))
            {
                var result = await _redis.JsonGetAsync(currency.ToString().ToLower());
                var coin = JsonConvert.DeserializeObject<CryptoCurrencyModel>(result.ToString());
                currencies.Add(coin);
            }
            return currencies;
        }

        public async Task<MarketChartModel> GetMarketChartAsync(CryptoCurrency currency)
        {
            var chart = await _redis.JsonGetAsync($"{currency.ToString().ToLower()}:market_chart");
            return JsonConvert.DeserializeObject<MarketChartModel>(chart.ToString());
        }
    }
}

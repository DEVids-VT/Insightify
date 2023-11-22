using AutoMapper;
using Insightify.Web.Gateway.Clients;
using Insightify.Web.Gateway.Infrastructure.Enums;
using Insightify.Web.Gateway.Infrastructure.Exceptions;
using Insightify.Web.Gateway.Models.FinancialData;

namespace Insightify.Web.Gateway.Services.FinancialData
{
    public class FinancialDataService : IFinancialDataService
    {
        private readonly IFinancialDataClient _financialDataClient;
        private readonly IMapper _mapper;

        public FinancialDataService(IFinancialDataClient financialDataClient, IMapper mapper)
        {
            _financialDataClient = financialDataClient;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CryptoCurrencyOutputModel>> GetAllCurrencies()
        {
            var currenciesResponse = await _financialDataClient.GetAllCurrencies();
            var currencies = currenciesResponse.Content;

            if (currencies == null)
            {
                throw new NotFoundException();
            }
            var currenciesOut = _mapper.Map<List<CryptoCurrencyOutputModel>>(currencies);
            return currenciesOut;
        }
        public async Task<CryptoCurrencyOutputModel> GetCurrency(CryptoCurrency currency)
        {
            var currencyResponse = await _financialDataClient.GetCurrency(currency.ToString().ToLower());
            var coin = currencyResponse.Content;

            if (coin == null)
            {
                throw new NotFoundException();
            }
            var currencyOut = _mapper.Map<CryptoCurrencyOutputModel>(coin);
            return currencyOut;
        }
        public async Task<MarketChartOutputModel> GetMarketChart(CryptoCurrency currency)
        {
            var chartResponse = await _financialDataClient.GetMarketChart(currency.ToString().ToLower());
            var chart = chartResponse.Content;

            if (chart == null)
            {
                throw new NotFoundException();
            }
            var chartOut = _mapper.Map<MarketChartOutputModel>(chart);
            return chartOut;
        }

    }
}

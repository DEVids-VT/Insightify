using AutoMapper;
using Insightify.Framework.Pagination;
using Insightify.Framework.Pagination.Abstractions;
using Insightify.Framework.Pagination.Headers;
using Insightify.MVC.Clients;
using Insightify.MVC.Infrastructure.Exceptions;
using Insightify.MVC.Infrastructure.Pagination;
using Insightify.MVC.Models;
using Insightify.MVC.Models.FinancialData;
using Insightify.Web.Gateway.Models.FinancialData;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Insightify.MVC.Services.FinancialData
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

        public async Task<MarketChartModel> Chart(string currency)
        {
            var chartResponce = await _financialDataClient.Chart(currency);

            if (chartResponce == null || chartResponce.Content == null)
            {
                throw new NotFoundException();
            }

            return chartResponce.Content;
        }

        public async Task<CryptoCurrencyModel> Currency(string currency)
        {
            var currencyResponce = await _financialDataClient.Currency(currency);

            if (currencyResponce == null || currencyResponce.Content == null)
            {
                throw new NotFoundException();
            }

            return currencyResponce.Content;
        }

        public async Task<DashboardModel> Dashboard()
        {
            var chart = await Chart("bitcoin");
            var btc = await Currency("bitcoin");
            var data = new List<DashboardCurrencyModel>
            {
                new DashboardCurrencyModel
                {
                    Name = btc.Name, 
                    CurrentPrice = double.Parse(btc.MarketData.CurrentPrice.Usd.Value.ToString()), 
                    Image = btc.Image.Large, 
                    PriceChange = btc.MarketData.PriceChangePercentage24h.Value
                },
                new DashboardCurrencyModel
                {
                    Name = btc.Name,
                    CurrentPrice = double.Parse(btc.MarketData.CurrentPrice.Usd.Value.ToString()),
                    Image = btc.Image.Large,
                    PriceChange = btc.MarketData.PriceChangePercentage24h.Value
                },
                new DashboardCurrencyModel
                {
                    Name = btc.Name,
                    CurrentPrice = double.Parse(btc.MarketData.CurrentPrice.Usd.Value.ToString()),
                    Image = btc.Image.Large,
                    PriceChange = btc.MarketData.PriceChangePercentage24h.Value
                },
                new DashboardCurrencyModel
                {
                    Name = btc.Name,
                    CurrentPrice = double.Parse(btc.MarketData.CurrentPrice.Usd.Value.ToString()),
                    Image = btc.Image.Large,
                    PriceChange = btc.MarketData.PriceChangePercentage24h.Value
                }
            };

            return new DashboardModel { ChartData = chart, Currencies = data };
        }

        public async Task<IEnumerable<CryptoCurrencyModel>> GetAllCurrencies()
        {
            var financialDataResponce = await _financialDataClient.AllCurrencies();

            var data = financialDataResponce.Content;

            if (data == null)
            {
                throw new NotFoundException();
            }

            var dataOut = _mapper.Map<List<CryptoCurrencyModel>>(data);
            return dataOut;
        }
    }
}

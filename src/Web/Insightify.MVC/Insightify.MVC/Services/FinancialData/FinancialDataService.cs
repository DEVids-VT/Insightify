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

        public async Task<DashboardModel> DashboardCurrency(string currency)
        {
            var chart = await Chart(currency);
            var currencyData = await Currency(currency);

            return new DashboardModel { ChartData = chart, Currency = currencyData };
        }

        public async Task<IPage<CryptoCurrencyModel>> GetAllCurrencies(string? title = null, int pageIndex = 1, int pageSize = 50)
        {
            var financialDataResponce = await _financialDataClient.AllCurrencies(title, pageIndex, pageSize);

            var paginationHeaders =
                financialDataResponce.Headers.TryGetValues(PaginationHeaderNames.PaginationHeaderName.ToLower(),
                    out IEnumerable<string>? headers);

            var parsedHeaders = HeaderHelpers.ParseHeader(headers?.ToList()[0]);
            var data = financialDataResponce.Content;

            if (data == null || parsedHeaders == null)
            {
                throw new NotFoundException();
            }

            var dataOut = _mapper.Map<List<CryptoCurrencyModel>>(data);
            return new Page<CryptoCurrencyModel>(
                dataOut,
                parsedHeaders["CurrentPage"],
                parsedHeaders["PageSize"],
                parsedHeaders["TotalCount"]);
        }
    }
}

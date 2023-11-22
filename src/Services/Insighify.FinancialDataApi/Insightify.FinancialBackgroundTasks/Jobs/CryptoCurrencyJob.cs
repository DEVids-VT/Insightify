using Insightify.FinancialBackgroundTasks.Extensions;
using Insightify.FinancialBackgroundTasks.Infrastructure.Enums;
using Insightify.FinancialBackgroundTasks.Models;
using Insightify.Framework.Fetching.Interfaces;
using Insightify.Framework.Messaging.Abstractions.Interfaces;
using Newtonsoft.Json;
using NReJSON;
using Quartz;
using StackExchange.Redis;

namespace Insightify.FinancialBackgroundTasks.Jobs
{
    internal class CryptoCurrencyJob : IJob
    {
        private readonly IApiFetcher _fetcher;
        private readonly ILogger _logger;
        private readonly IMessagePublisher _publisher;
        private readonly IDatabase _redis;
        public CryptoCurrencyJob(IApiFetcher fetcher, ILogger<MarketChartsJob> logger, IMessagePublisher publisher, IDatabase redis)
        {
            _fetcher = fetcher;
            _logger = logger;
            _publisher = publisher;
            _redis = redis;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            foreach (CryptoCurrency currency in Enum.GetValues(typeof(CryptoCurrency)))
            {
                var queryParams = new Dictionary<string, string>()
                {
                    { "tickers", "false" },
                };
                var apiEndpoint = $"/api/v3/coins/{currency.GetID()}";
                try
                {
                    var response =
                        await _fetcher.FetchDataWithQueryAsync<CryptoCurrencyModel>(apiEndpoint, queryParams);
                    _redis.JsonSet($"{currency.GetID()}", JsonConvert.SerializeObject(response));

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}

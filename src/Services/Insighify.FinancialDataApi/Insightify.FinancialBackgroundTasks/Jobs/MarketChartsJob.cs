using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    [DisallowConcurrentExecution]
    public class MarketChartsJob : IJob
    {
        private readonly IApiFetcher _fetcher;
        private readonly ILogger _logger;
        private readonly IMessagePublisher _publisher;
        private readonly IDatabase _redis;
        public MarketChartsJob(IApiFetcher fetcher, ILogger<MarketChartsJob> logger, IMessagePublisher publisher, IDatabase redis)
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
                    { "vs_currency", "usd" },
                    { "days", "1" }
                };

                var apiEndpoint = $"/api/v3/coins/{currency.GetID()}/market_chart";
                var response = await _fetcher.FetchDataWithQueryAsync<MarketChartModel>(apiEndpoint, queryParams);

                _redis.JsonSet($"{currency.GetID()}:market_chart", JsonConvert.SerializeObject(response));
            }
        }
    }
}

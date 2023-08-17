using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.FinancialBackgroundTasks.Models;
using Insightify.Framework.Fetching.Interfaces;
using Insightify.Framework.Messaging.Abstractions.Interfaces;
using Newtonsoft.Json;
using NReJSON;
using Quartz;
using StackExchange.Redis;

namespace Insightify.FinancialBackgroundTasks.Jobs
{
    public class BitcoinMarketChartJob : IJob
    {
        private readonly IApiFetcher _fetcher;
        private readonly ILogger _logger;
        private readonly IMessagePublisher _publisher;
        private readonly IDatabase _redis;
        public BitcoinMarketChartJob(IApiFetcher fetcher, ILogger<BitcoinMarketChartJob> logger, IMessagePublisher publisher, IDatabase redis)
        {
            _fetcher = fetcher;
            _logger = logger;
            _publisher = publisher;
            _redis = redis;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var queryParams = new Dictionary<string, string>()
            {
                { "vs_currency", "usd" },
                { "days", "1" }
            };
            var response =
                await _fetcher.FetchDataWithQueryAsync<MarketChartModel>("/api/v3/coins/bitcoin/market_chart", queryParams);

            _redis.JsonSet("bitcoin:market_chart", JsonConvert.SerializeObject(response));
        }
    }
}

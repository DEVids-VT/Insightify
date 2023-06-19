using Insightify.Framework.Fetching.Interfaces;
using Insightify.Framework.Messaging.Abstractions.Interfaces;
using Insightify.NewsBackgroundTasks.Configuration;
using Insightify.NewsBackgroundTasks.Configuration.Enums;
using Insightify.NewsBackgroundTasks.Events;
using Insightify.NewsBackgroundTasks.ResponceModels.LiveNews;
using Insightify.NewsBackgroundTasks.Services.Contracts;
using MassTransit;
using Quartz;

namespace Insightify.NewsBackgroundTasks.Jobs
{
    public class NewsFetcherJob : IJob
    {
        private readonly IApiFetcher _fetcher;
        private readonly ILogger _logger;
        private readonly IMessagePublisher _publisher;
        private readonly INewsService _newsService;
        public NewsFetcherJob(IApiFetcher fetcher, ILogger<NewsFetcherJob> logger, IMessagePublisher publisher, INewsService newsService)
        {
            _fetcher = fetcher;
            _logger = logger;
            _publisher = publisher;
            _newsService = newsService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Fetching articles");

            var response = await _fetcher.FetchDataAsync<LiveNewsResponseModel>(UrlsConfig.LiveNewsOperations.GetLiveNews( DateTime.Now, NewsSort.popularity, "business"));
            var topArticle = response.Data.First();
            response.Data.ForEach(article => _newsService.Add(article));
            NotificationEvent @event = new()
            {
                Title = topArticle.Title,
                PublishTime = DateTime.Now,
                Summary = topArticle.Description,
                Source = topArticle.Source,
                ImageUrl = topArticle.Image,
                CreationDate = DateTime.Now,
                Id = Guid.NewGuid()
            };
            await _publisher.PublishAsync(@event);
        }
    }
}

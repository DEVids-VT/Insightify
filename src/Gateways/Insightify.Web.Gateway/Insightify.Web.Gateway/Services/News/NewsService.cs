using AutoMapper;
using Insightify.Web.Gateway.Clients;
using Insightify.Web.Gateway.Models;

namespace Insightify.Web.Gateway.Services.News
{
    public class NewsService : INewsService
    {
        private readonly INewsClient _newsClient;
        private readonly IMapper _mapper;

        public NewsService(INewsClient newsClient, IMapper mapper)
        {
            _newsClient = newsClient;
            _mapper = mapper;
        }
        public async Task<List<NewsArticleOutputModel>> GetArticles(int pageIndex = 1, int pageSize = 50)
        {
            var articlesResponse = await _newsClient.Articles(pageIndex, pageSize);
            var articles = _mapper.Map<List<NewsArticleOutputModel>>(articlesResponse);
            return articles;
        }
    }
}

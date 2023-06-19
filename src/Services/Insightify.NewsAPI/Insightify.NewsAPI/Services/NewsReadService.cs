using Insightify.NewsAPI.Infrastructure.Models;
using System.Linq.Expressions;
using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Insightify.NewsAPI.Pagination;

namespace Insightify.NewsAPI.Services
{
    public class NewsReadService : INewsReadService
    {
        private readonly ILogger _logger;
        private readonly IRepository<NewsArticleModel> _repo;
        public NewsReadService(ILogger<NewsReadService> logger, IRepository<NewsArticleModel> repository)
        {
            _logger = logger;
            _repo = repository;
        }
        public async Task<IPagedList<NewsArticleModel>> GetArticlesAsync(int pageNumber, int pageSize = 10)
        {
            var articles = await _repo.GetPagedListAsync(pageNumber, pageSize);
            if (articles == null || articles.TotalCount == 0)
            {
                return null;
            }
            return articles;
        }
        public async Task<List<NewsArticleModel>> GetAllAsync()
        {
            _logger.LogInformation($"Receiving all documents");
            var articles = await _repo.GetAllAsync();
            return articles;
        }

        public async Task<List<NewsArticleModel>> GetAllAsync(Expression<Func<NewsArticleModel, bool>> predicate)
        {
            _logger.LogInformation($"Receiving all documents maching predicate");
            var articles = await _repo.GetAllAsync(predicate);
            return articles;
        }

        public async Task<NewsArticleModel> GetByIdAsync(string id, bool includeDeleted = false)
        {
            _logger.LogInformation($"Receiving the document with id {id}");
            var article = await _repo.GetFirstOrDefaultAsync(x => x.Id == id, includeDeleted: includeDeleted);
            return article;
        }

    }
}

using System.Linq.Expressions;
using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Insightify.NewsAPI.Infrastructure.Models;

namespace Insightify.NewsAPI.Services
{
    public interface INewsReadService
    {
        Task<List<NewsArticleModel>> GetAllAsync();

        Task<List<NewsArticleModel>> GetAllAsync(Expression<Func<NewsArticleModel, bool>> predicate);

        Task<NewsArticleModel> GetByIdAsync(string id, bool includeDeleted = false);
        Task<IPagedList<NewsArticleModel>> GetArticlesAsync(int pageNumber, int pageSize = 10);
    }
}

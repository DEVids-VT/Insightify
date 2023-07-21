using Insightify.Web.Gateway.Models;
using Refit;

namespace Insightify.Web.Gateway.Services.News
{
    public interface INewsService
    {
        Task<List<NewsArticleOutputModel>> GetArticles(int pageIndex = 1, int pageSize = 50);
    }
}

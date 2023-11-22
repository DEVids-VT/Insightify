using Insightify.Framework.Pagination.Abstractions;
using Insightify.Web.Gateway.Models;
using Refit;

namespace Insightify.Web.Gateway.Services.News
{
    public interface INewsService
    {
        Task<IPage<NewsArticleOutputModel>> GetArticles(int pageIndex = 1, int pageSize = 50);
    }
}

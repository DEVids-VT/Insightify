using Insightify.Framework.Pagination.Abstractions;
using Insightify.MVC.Models;

namespace Insightify.MVC.Services.News
{
    public interface INewsService
    {
        Task<IPage<NewsViewModel>> GetNews(string? title = null, int pageIndex = 1, int pageSize = 50);
    }
}

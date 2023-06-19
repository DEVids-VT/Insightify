using Insightify.Framework.MongoDb.Abstractions.Interfaces;

namespace Insightify.NewsAPI.Pagination
{
    public static class PagedListExtensions
    {
        public static IPage<T> ToPage<T>(this IPagedList<T> pagedList) => new Page<T>(
            pagedList.Items,
            pagedList.PageIndex,
            pagedList.PageSize,
            (int)pagedList.TotalCount);
    }
}

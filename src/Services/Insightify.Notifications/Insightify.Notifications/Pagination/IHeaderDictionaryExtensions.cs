using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Insightify.NotificationsAPI.Pagination
{
    public static class IHeaderDictionaryExtensions
    {
        public static void AddPaginationHeader(this IHeaderDictionary headers, int currentPage, int pageSize, int totalCount)
        {
            var paginationHeaderValue = new PaginationHeaderValue
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = CalculateTotalPages(pageSize, totalCount)
            };

            headers[PaginationHeaderNames.PaginationHeaderName] = JsonConvert.SerializeObject(paginationHeaderValue);
        }
        private static int CalculateTotalPages(int pageSize, int totalCount)
        {
            if (pageSize == 0)
            {
                return default;
            }

            var totalPages = totalCount / pageSize;

            if (totalCount % pageSize != 0)
            {
                totalPages++;
            }

            return totalPages;
        }
    }
}

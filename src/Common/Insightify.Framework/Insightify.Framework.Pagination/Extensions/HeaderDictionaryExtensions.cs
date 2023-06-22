using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.Pagination.Headers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Insightify.Framework.Pagination.Extensions
{
    public static class HeaderDictionaryExtensions
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

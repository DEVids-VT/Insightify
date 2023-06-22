using Insightify.Framework.Pagination.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.Pagination.Extensions
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

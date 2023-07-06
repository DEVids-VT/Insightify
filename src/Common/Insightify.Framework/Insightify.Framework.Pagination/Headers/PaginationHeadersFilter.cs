using Insightify.Framework.Pagination.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.Pagination.Extensions;

namespace Insightify.Framework.Pagination.Headers
{
    public class PaginationHeadersFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(
            ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult
                && objectResult.Value is IPage page)
            {
                context.HttpContext.Response.Headers.AddPaginationHeader(
                    page.CurrentPage,
                    page.PageSize,
                    page.TotalCount);
            }

            await next();
        }
    }
}

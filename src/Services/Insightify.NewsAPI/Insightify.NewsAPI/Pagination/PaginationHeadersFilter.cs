using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.NewsAPI.Pagination
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

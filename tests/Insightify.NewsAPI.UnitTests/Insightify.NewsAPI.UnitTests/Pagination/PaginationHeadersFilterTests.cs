using Insightify.NewsAPI.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace Insightify.NewsAPI.UnitTests.Pagination
{
    public class PaginationHeadersFilterTests
    {
        [Fact]
        public async Task OnResultExecutionAsync_AddsPaginationHeaders_WhenIPageIsReturned()
        {
            // Arrange
            var headers = new HeaderDictionary();
            var response = new Mock<HttpResponse>();
            response.SetupGet(r => r.Headers).Returns(headers);

            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(hc => hc.Response).Returns(response.Object);

            var actionResult = new ObjectResult(new Page<string>(new List<string>(), 1, 10, 100))
            {
                StatusCode = 200
            };

            var context = new ResultExecutingContext(
                new ActionContext
                {
                    HttpContext = httpContext.Object,
                    RouteData = new RouteData(),
                    ActionDescriptor = new ActionDescriptor()
                },
                new List<IFilterMetadata>(),
                actionResult,
                new object());

            var next = new Mock<ResultExecutionDelegate>();
            var filter = new PaginationHeadersFilter();

            // Act
            await filter.OnResultExecutionAsync(context, next.Object);

            // Assert
            Assert.True(headers.ContainsKey("X-Pagination"));
            
            Assert.Equal("{\"CurrentPage\":1,\"PageSize\":10,\"TotalPages\":10,\"TotalCount\":100}", headers["X-Pagination"].ToString());
        }
    }
}

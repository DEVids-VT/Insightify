using AutoMapper;
using Insightify.Framework.Pagination;
using Insightify.Framework.Pagination.Abstractions;
using Insightify.Framework.Pagination.Headers;
using Insightify.MVC.Clients;
using Insightify.MVC.Infrastructure.Exceptions;
using Insightify.MVC.Infrastructure.Pagination;
using Insightify.MVC.Models;

namespace Insightify.MVC.Services.News
{
    public class NewsService : INewsService
    {
        private readonly INewsClient _newsClient;
        private readonly IMapper _mapper;

        public NewsService(INewsClient newsClient, IMapper mapper)
        {
            _newsClient = newsClient;
            _mapper = mapper;
        }

        public async Task<IPage<NewsViewModel>> GetNews(string? title = null, int pageIndex = 1, int pageSize = 50)
        {
            var newsResponce = await _newsClient.News(title, pageIndex, pageSize);

            var paginationHeaders =
                newsResponce.Headers.TryGetValues(PaginationHeaderNames.PaginationHeaderName.ToLower(),
                    out IEnumerable<string>? headers);

            var parsedHeaders = HeaderHelpers.ParseHeader(headers?.ToList()[0]);
            var news = newsResponce.Content;

            if (news == null || parsedHeaders == null)
            {
                throw new NotFoundException();
            }

            var newsOut = _mapper.Map<List<NewsViewModel>>(news);
            return new Page<NewsViewModel>(
                newsOut,
                parsedHeaders["CurrentPage"],
                parsedHeaders["PageSize"],
                parsedHeaders["TotalCount"]);
        }
    }
}

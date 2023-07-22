using AutoMapper;
using Insightify.Framework.Pagination;
using Insightify.Framework.Pagination.Abstractions;
using Insightify.Framework.Pagination.Headers;
using Insightify.Web.Gateway.Clients;
using Insightify.Web.Gateway.Clients.Models;
using Insightify.Web.Gateway.Infrastructure.Exceptions;
using Insightify.Web.Gateway.Infrastructure.Pagination;
using Insightify.Web.Gateway.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace Insightify.Web.Gateway.Services.News
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
        public async Task<IPage<NewsArticleOutputModel>> GetArticles(int pageIndex = 1, int pageSize = 50)
        {
            var articlesResponse = await _newsClient.Articles(pageIndex, pageSize);

            var paginationHeaders =
                articlesResponse.Headers.TryGetValues(PaginationHeaderNames.PaginationHeaderName.ToLower(),
                    out IEnumerable<string>? headers);

            var parsedHeaders = HeaderHelpers.ParseHeader(headers?.ToList()[0]);
            var articles = articlesResponse.Content;

            if (articles == null || parsedHeaders == null)
            {
                throw new NotFoundException();
            }

            var articlesOut = _mapper.Map<List<NewsArticleOutputModel>>(articles);
            return new Page<NewsArticleOutputModel>(
                articlesOut, 
                parsedHeaders["CurrentPage"], 
                parsedHeaders["PageSize"], 
                parsedHeaders["TotalCount"]);
        }
    }
}

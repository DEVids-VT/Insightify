using AutoMapper;
using Insightify.Framework.Pagination;
using Insightify.Framework.Pagination.Abstractions;
using Insightify.Framework.Pagination.Headers;
using Insightify.Web.Gateway.Clients;
using Insightify.Web.Gateway.Infrastructure.Exceptions;
using Insightify.Web.Gateway.Infrastructure.Pagination;
using Insightify.Web.Gateway.Models;

namespace Insightify.Web.Gateway.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IPostsClient _postClient;
        private readonly IMapper _mapper;

        public PostService(IPostsClient postClient, IMapper mapper)
        {
            _postClient = postClient;
            _mapper = mapper;
        }

        public async Task<IPage<PostOutputModel>> GetPosts(string? title = null, int pageIndex = 1, int pageSize = 50)
        {
            var postsResponse = await _postClient.Posts(title, pageIndex, pageSize);

            var paginationHeaders = 
                postsResponse.Headers.TryGetValues(PaginationHeaderNames.PaginationHeaderName.ToLower(),
                    out IEnumerable<string>? headers);

            var parsedHeaders = HeaderHelpers.ParseHeader(headers?.ToList()[0]);
            var posts = postsResponse.Content;

            if (posts == null || parsedHeaders == null)
            {
                throw new NotFoundException();
            }

            var postsOut = _mapper.Map<List<PostOutputModel>>(posts);
            return new Page<PostOutputModel>(
                postsOut,
                parsedHeaders["CurrentPage"],
                parsedHeaders["PageSize"],
                parsedHeaders["TotalCount"]);
        }
    }
}

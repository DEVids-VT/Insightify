using AutoMapper;
using Insightify.Framework.Pagination;
using Insightify.Framework.Pagination.Abstractions;
using Insightify.Framework.Pagination.Headers;
using Insightify.MVC.Clients;
using Insightify.MVC.Infrastructure.Exceptions;
using Insightify.MVC.Infrastructure.Pagination;
using Insightify.MVC.Models;

namespace Insightify.MVC.Services.Posts
{
    public class PostsService : IPostsService
    {
        private readonly IPostsClient _postClient;
        private readonly IMapper _mapper;

        public PostsService(IPostsClient postClient, IMapper mapper)
        {
            _postClient = postClient;
            _mapper = mapper;
        }

        public async Task<IPage<PostViewModel>> GetPosts(string? title = null, int pageIndex = 1, int pageSize = 50)
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

            var postsOut = _mapper.Map<List<PostViewModel>>(posts);
            return new Page<PostViewModel>(
                postsOut,
                parsedHeaders["CurrentPage"],
                parsedHeaders["PageSize"],
                parsedHeaders["TotalCount"]);
        }
    }
}

using AutoMapper;
using FluentValidation;
using Insightify.Framework.Pagination;
using Insightify.Framework.Pagination.Abstractions;
using Insightify.Framework.Pagination.Headers;
using Insightify.Web.Gateway.Clients;
using Insightify.Web.Gateway.Clients.Models.Posts;
using Insightify.Web.Gateway.Infrastructure.Exceptions;
using Insightify.Web.Gateway.Infrastructure.Pagination;
using Insightify.Web.Gateway.Models;
using Insightify.Web.Gateway.Models.Posts;
using System.Security.Claims;

namespace Insightify.Web.Gateway.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IPostsClient _postClient;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePostInputModel> _createPostValidator;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostService(IPostsClient postClient, IMapper mapper, IValidator<CreatePostInputModel> validator, ILogger<PostService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _postClient = postClient;
            _mapper = mapper;
            _createPostValidator = validator;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<CreatePostOutputModel> CreatePost(CreatePostInputModel post)
        {
            var validationResult = await _createPostValidator.ValidateAsync(post);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    _logger.LogError($"{error.ErrorMessage} for {error.AttemptedValue}");
                }

                throw new ArgumentException();
            }

            var postRequest = _mapper.Map<CreatePostRequestModel>(post);
            var responseModel = (await _postClient.Create(postRequest)).Content;
            var response = _mapper.Map<CreatePostOutputModel>(responseModel);

            return response;
        }

        public async Task<int> LikePost(int postId)
        {
            var response = await _postClient.Likes(postId);
            var likes = response.Content;
            var userId = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (likes.Any(l => l.UserId == userId))
            {
                await _postClient.Dislike(postId);
            }
            else
            {
                await _postClient.Like(postId);
            }
            var likeCount = (await _postClient.Likes(postId)).Content.Count;
            return likeCount;
        }

        public async Task<IEnumerable<LikeOutputModel>> Likes(int postId)
        {
            var response = await _postClient.Likes(postId);
            var likes = response.Content;
            var likesOut = _mapper.Map<List<LikeOutputModel>>(likes);
            return likesOut;
        }
    }
}

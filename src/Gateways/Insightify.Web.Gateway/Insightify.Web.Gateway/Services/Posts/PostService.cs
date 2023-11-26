using AutoMapper;
using FluentValidation;
using IdentityModel.OidcClient;
using Insightify.Framework.Pagination;
using Insightify.Framework.Pagination.Abstractions;
using Insightify.Framework.Pagination.Headers;
using Insightify.Web.Gateway.Clients;
using Insightify.Web.Gateway.Clients.Models.Posts;
using Insightify.Web.Gateway.Infrastructure.Exceptions;
using Insightify.Web.Gateway.Infrastructure.Pagination;
using Insightify.Web.Gateway.Models;
using Insightify.Web.Gateway.Models.Posts;
using Serilog.Sinks.Http.Private.Time;

namespace Insightify.Web.Gateway.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IPostsClient _postClient;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePostInputModel> _createPostValidator;
        private readonly ILogger _logger;

        public PostService(IPostsClient postClient, IMapper mapper, IValidator<CreatePostInputModel> validator, ILogger<PostService> logger)
        {
            _postClient = postClient;
            _mapper = mapper;
            _createPostValidator = validator;
            _logger = logger;
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

        public async Task LikePost(int postId)
        {
            await _postClient.Like(postId);
        }
        public async Task DislikePost(int postId)
        {
            await _postClient.Dislike(postId);
        }
        public async Task SavePost(int postId)
        {
            await _postClient.Save(postId);
        }
        public async Task UnsavePost(int postId)
        {
            await _postClient.Unsave(postId);
        }

        //public async Task CommentOnPost(int postId, string content)
        //{
        //    await _postClient.Comment(postId, content);
        //}
        //public async Task RemoveCommentOnPost(int commentId, int postId)
        //{
        //    await _postClient.RemoveComment(commentId, commentId);
        //}

    }
}

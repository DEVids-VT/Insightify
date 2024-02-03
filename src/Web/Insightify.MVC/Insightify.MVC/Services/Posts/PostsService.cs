using AutoMapper;
using IdentityModel.OidcClient;
using Insightify.Framework.Pagination;
using Insightify.Framework.Pagination.Abstractions;
using Insightify.Framework.Pagination.Headers;
using Insightify.MVC.Clients;
using Insightify.MVC.Clients.Models;
using Insightify.MVC.Infrastructure.Exceptions;
using Insightify.MVC.Infrastructure.Pagination;
using Insightify.MVC.Models.Posts;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace Insightify.MVC.Services.Posts
{
    public class PostsService : IPostsService
    {
        private readonly IPostsClient _postClient;
        private readonly IProfilesClient _profilesClient;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public PostsService(IPostsClient postClient, IProfilesClient profilesClient, IMapper mapper, HttpClient httpClient)
        {
            _postClient = postClient;
            _profilesClient = profilesClient;
            _mapper = mapper;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Client-ID", "41519381aee37da");
        }

        public async Task<CreatePostResponseModel> CreatePost(CreatePostInputModel model)
        {
            var imageUrl = await UploadImage.ToImgur(model.Image, _httpClient);

            return await CreatePostWithImageUrl(model, imageUrl);
        }

        public async Task<PostViewModel> GetPost(int postId)
        {
            var postResponse = await _postClient.Post(postId);
            var post = postResponse.Content;
            if (post == null)
            {
                throw new NotFoundException();

            }
            var postOut = _mapper.Map<PostViewModel>(post);

            var user = await _profilesClient.Profile(post.AuthorId);

            if (user != null && user.Content != null)
            {
                postOut.UserImg = user.Content.Img;
                postOut.Username = user.Content.Username;
            }

            return postOut;
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

            foreach (var post in postsOut)
            {
                var user = await _profilesClient.Profile(post.AuthorId);

                if(user != null && user.Content != null)
                {
                    post.UserImg = user.Content.Img;
                    post.Username = user.Content.Username;
                }
            }

            return new Page<PostViewModel>(
                postsOut,
                parsedHeaders["CurrentPage"],
                parsedHeaders["PageSize"],
                parsedHeaders["TotalCount"]);
        }

        public async Task<int> LikePost(int postId)
        {
           var likeCount = await _postClient.Like(postId);
           return likeCount;
        }

        public async Task<IEnumerable<LikeViewModel>> Likes(int postId)
        {
            var response = await _postClient.Likes(postId);
            var likes = response.Content;
            var likesView = _mapper.Map<List<LikeViewModel>>(likes);
            return likesView;
        }

        private async Task<CreatePostResponseModel> CreatePostWithImageUrl(CreatePostInputModel model, string imageUrl)
        {
            var postModel = new PostModel
            {
                Description = model.Description,
                Title = model.Title,
                ImageUrl = imageUrl
            };

            var result = await _postClient.Create(postModel);
            return result.Content!;
        }
    }
}

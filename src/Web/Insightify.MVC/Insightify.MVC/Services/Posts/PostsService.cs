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
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public PostsService(IPostsClient postClient, IMapper mapper, HttpClient httpClient)
        {
            _postClient = postClient;
            _mapper = mapper;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Client-ID", "41519381aee37da");
        }

        public async Task<CreatePostResponseModel> CreatePost(CreatePostInputModel model)
        {
            var imageUrl = await UploadImageToImgur(model.Image);

            return await CreatePostWithImageUrl(model, imageUrl);
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

        private async Task<string> UploadImageToImgur(IFormFile imageFile)
        {
            using var formContent = new MultipartFormDataContent();
            using var imageStream = imageFile.OpenReadStream();
            using var streamContent = new StreamContent(imageStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);

            formContent.Add(streamContent, "image", imageFile.FileName);

            var response = await _httpClient.PostAsync("https://api.imgur.com/3/upload", formContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Image upload failed with status code: {response.StatusCode}");
            }

            var jsonResponse = JObject.Parse(responseContent);
            return jsonResponse["data"]["link"].ToString();
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

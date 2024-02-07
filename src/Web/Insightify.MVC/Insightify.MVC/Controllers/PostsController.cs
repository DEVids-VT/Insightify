using Insightify.Framework.Pagination;
using Insightify.MVC.Models.Posts;
using Insightify.MVC.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Text;

namespace Insightify.MVC.Controllers
{
    [AllowAnonymous]
    public class PostsController : Controller
    {
        private readonly IPostsService _postService;
        public PostsController(IPostsService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Feed([FromQuery] string? title = null, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 50, [FromQuery] bool json = false)
        {
            var result = await _postService.GetPosts(title, pageIndex, pageSize);

            //await Task.Delay(1000);

            //var postViewModelList = new List<PostViewModel>();
            //var random = new Random();
            //string[] words = { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew", "indian plum", "jackfruit", "kiwi", "lemon", "mango", "nectarine", "orange", "papaya", "quince", "raspberry", "strawberry", "tangerine", "ugli fruit", "vanilla", "watermelon", "xylocarp", "yellow passionfruit", "zucchini" };

            //for (int i = 0; i < 20; i++)
            //{
            //    int wordCount = random.Next(50, 500 + 1);
            //    var stringBuilder = new StringBuilder();

            //    for (int j = 0; j < wordCount; j++)
            //    {
            //        int index = random.Next(words.Length);
            //        stringBuilder.Append(words[index]);
            //        stringBuilder.Append(' ');
            //    }

            //    postViewModelList.Add(new PostViewModel
            //    {
            //        Id = i + 1,
            //        AuthorId = (i + 1).ToString(),
            //        CommentCount = i * 5,
            //        Description = stringBuilder.ToString(),
            //        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b6/Image_created_with_a_mobile_phone.png",
            //        LikeCount = i * 100,
            //        SaveCount = i * 20,
            //        Title = "Title " + (i + 1)
            //    });
            //}

            //var result = new Page<PostViewModel>(postViewModelList, 1, 10, 20);

            return json ? Json(result) : View(result);
        }
        [HttpGet("post/{id}")]
        public async Task<IActionResult> ViewPost(int id)
        {
            /*var post = new PostViewModel 
            { 
                Id = id,
                Comments = new List<CommentViewModel>()
                {
                    new CommentViewModel
                    {
                        Content = "asda asd asd asdasd",
                        Username = "asdasd", 
                        UserPfp = "asdasd"
                    }
                },
                Description = "asdasd asd asd asd asd asdasd asd",
                Title = "Title",
                Username = "asdasdasdasdda",
                Tags = new List<string>() { "sd", "asdasd", "asdasd" }
            };   */
            var post = await _postService.GetPost(id);
            var comments = await _postService.Comments(id);
            post.Comments = comments;
            return View(post);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreatePostInputModel postData)
        {
            var model = await _postService.CreatePost(postData);

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> Like(int postId)
        {
            var likeCount = await _postService.LikePost(postId);
            return Ok(likeCount);
        }
        [HttpPost]
        public async Task<IActionResult> Comment([FromBody] CreateCommentInputModel comment)
        {
            await _postService.Comment(comment);
            return Ok();
        }
    }
}

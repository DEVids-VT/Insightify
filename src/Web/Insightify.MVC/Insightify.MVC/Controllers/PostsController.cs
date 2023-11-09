using Insightify.Framework.Pagination;
using Insightify.MVC.Models;
using Insightify.MVC.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Insightify.MVC.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostsService _postService;
        public PostsController(IPostsService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        public async Task<IActionResult> Feed([FromQuery] string? title = null, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 50, bool json = false)
        {
            //var result = await _postService.GetPosts(title, pageIndex, pageSize);

            await Task.Delay(1000);

            var postViewModelList = new List<PostViewModel>();
            var random = new Random();
            string[] words = { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew", "indian plum", "jackfruit", "kiwi", "lemon", "mango", "nectarine", "orange", "papaya", "quince", "raspberry", "strawberry", "tangerine", "ugli fruit", "vanilla", "watermelon", "xylocarp", "yellow passionfruit", "zucchini" };

            for (int i = 0; i < 20; i++)
            {
                int wordCount = random.Next(50, 500 + 1);
                var stringBuilder = new StringBuilder();

                for (int j = 0; j < wordCount; j++)
                {
                    int index = random.Next(words.Length);
                    stringBuilder.Append(words[index]);
                    stringBuilder.Append(' ');
                }

                postViewModelList.Add(new PostViewModel
                {
                    Id = i + 1,
                    AuthorId = (i + 1).ToString(),
                    CommentCount = i * 5,
                    Description = stringBuilder.ToString(),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b6/Image_created_with_a_mobile_phone.png",
                    LikeCount = i * 100,
                    SaveCount = i * 20,
                    Title = "Title " + (i + 1)
                });
            }

            var result = new Page<PostViewModel>(postViewModelList, 1, 10, 20);

            return json ? Json(result) : View(result);
        }
    }
}

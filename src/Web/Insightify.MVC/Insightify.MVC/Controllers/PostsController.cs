using Insightify.Framework.Pagination;
using Insightify.MVC.Models;
using Insightify.MVC.Services.Posts;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Feed([FromQuery] string? title = null, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 50)
        {
            //var result = await _postService.GetPosts(title, pageIndex, pageSize);

            var postViewModelList = new List<PostViewModel>();

            for (int i = 0; i < 20; i++)
            {
                postViewModelList.Add(new PostViewModel
                {
                    Id = i + 1,
                    AuthorId = (i + 1).ToString(),
                    CommentCount = i * 5,
                    Description = "Lorem ipsum is placeholder text commonly used in the graphic, print, " +
                                  "and publishing industries for previewing layouts and visual mockups.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b6/Image_created_with_a_mobile_phone.png",
                    LikeCount = i * 100,
                    SaveCount = i * 20,
                    Title = "Title " + (i + 1)
                });
            }

            var result = new Page<PostViewModel>(postViewModelList, 1, 10, 20);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> FeedJson([FromQuery] string? title = null, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 50)
        {
            //var result = await _postService.GetPosts(title, pageIndex, pageSize);
            await Task.Delay(1000);
            var postViewModelList = new List<PostViewModel>();

            for (int i = 0; i < 20; i++)
            {
                postViewModelList.Add(new PostViewModel
                {
                    Id = i + 1,
                    AuthorId = (i + 1).ToString(),
                    CommentCount = i * 5,
                    Description = "Lorem ipsum is placeholder text commonly used in the graphic, print, " +
                                  "and publishing industries for previewing layouts and visual mockups.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b6/Image_created_with_a_mobile_phone.png",
                    LikeCount = i * 100,
                    SaveCount = i * 20,
                    Title = "Title " + (i + 1)
                });
            }

            var result = new Page<PostViewModel>(postViewModelList, 1, 10, 20);

            return Json(result);
        }
    }
}

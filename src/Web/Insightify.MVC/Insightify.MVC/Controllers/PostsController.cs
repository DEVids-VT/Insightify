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
            var result = await _postService.GetPosts(title, pageIndex, pageSize);

            return View(result);
        }
    }
}

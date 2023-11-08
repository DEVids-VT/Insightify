using Insightify.Web.Gateway.Services.News;
using Insightify.Web.Gateway.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.Web.Gateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> Posts([FromQuery] string? title = null, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 50)
        {
            var result = await _postService.GetPosts(title, pageIndex, pageSize);
            return result != null ? Ok(result) : NotFound();
        }
    }
}

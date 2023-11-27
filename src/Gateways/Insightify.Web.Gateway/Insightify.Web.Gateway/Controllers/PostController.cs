using Insightify.Web.Gateway.Models;
using Insightify.Web.Gateway.Models.Posts;
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

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreatePostInputModel post)
        {
            var response = await _postService.CreatePost(post);
            return Ok(response);
        }

        [HttpPost]
        [Route("{postId}/like")]
        public async Task<IActionResult> Like([FromRoute] int postId)
        {
            await _postService.LikePost(postId);
            return Ok();
        }
    }
}

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

        [HttpGet]
        [Route("{postId}")]
        public async Task<IActionResult> Post([FromRoute] int postId)
        {
            var result = await _postService.GetPost(postId);
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
            var likeCount = await _postService.LikePost(postId);
            return Ok(likeCount);
        }
        [HttpGet]
        [Route("{postId}/likes")]
        public async Task<IActionResult> Likes([FromRoute] int postId)
        {
            var likes = await _postService.Likes(postId);
            return Ok(likes);
        }

        [HttpPost]
        [Route("comment")]
        public async Task<IActionResult> Comment([FromBody] CreateCommentInputModel comment)
        {
            await _postService.Comment(comment);
            return Ok();
        }

        [HttpGet]
        [Route("{postId}/comments")]
        public async Task<IActionResult> Comments([FromRoute] int postId)
        {
            var comments = await _postService.Comments(postId);
            return Ok(comments);
        }
    }
}

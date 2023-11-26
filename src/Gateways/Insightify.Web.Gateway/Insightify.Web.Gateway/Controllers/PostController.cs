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
        [HttpPost]
        [Route("{postId}/dislike")]
        public async Task<IActionResult> Dislike([FromRoute] int postId)
        {
            await _postService.DislikePost(postId);
            return Ok();
        }
        [HttpPost]
        [Route("{postId}/save")]
        public async Task<IActionResult> Save([FromRoute] int postId)
        {
            await _postService.SavePost(postId);
            return Ok();
        }
        [HttpPost]
        [Route("{postId}/unsave")]
        public async Task<IActionResult> Unsave([FromRoute] int postId)
        {
            await _postService.UnsavePost(postId);
            return Ok();
        }
        //[HttpPost]
        //[Route("comment")]
        //public async Task<IActionResult> Comment([FromBody] int postId, string content)
        //{
        //    await _postService.CommentOnPost(postId, content);
        //    return Ok();
        //}
        //[HttpPost]
        //[Route("uncomment")]
        //public async Task<IActionResult> Uncomment([FromBody] int commentId, int postId)
        //{
        //    await _postService.RemoveCommentOnPost(commentId, postId);
        //    return Ok();
        //}
    }
}

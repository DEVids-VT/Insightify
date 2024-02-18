using Insightify.Framework.Pagination;
using Insightify.MVC.Models.Posts;
using Insightify.MVC.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refit;
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
        public async Task<IActionResult> Feed([FromQuery] string? title = null, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10, [FromQuery] bool json = false)
        {
            var result = await _postService.GetPosts(title, pageIndex, pageSize);

            
            return json ? Json(result) : View(result);
        }
        [HttpGet("post/{id}")]
        public async Task<IActionResult> ViewPost(int id)
        {
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

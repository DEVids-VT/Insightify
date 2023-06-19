using Insightify.NewsAPI.Pagination;
using Insightify.NewsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Insightify.NewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsReadService _newsService;

        public NewsController(INewsReadService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("{newsArticleId}")]
        public async Task<IActionResult> GetNewsArticle(string newsArticleId, [FromQuery] bool includeDeleted)
        {
            var result = await _newsService.GetByIdAsync(newsArticleId, includeDeleted);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("articles")]
        public async Task<IActionResult> GetArticles(
            [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
            int pageIndex = 1,
            [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
            int pageSize = 50)
        {
            var result = await _newsService.GetArticlesAsync(pageIndex, pageSize);
            return result != null ? Ok(result.ToPage()) : NotFound();

        }

    }
}

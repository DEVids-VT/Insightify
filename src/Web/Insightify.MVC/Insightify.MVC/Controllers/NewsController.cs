using Insightify.Framework.Pagination;
using Insightify.MVC.Models;
using Insightify.MVC.Services.News;
using Insightify.MVC.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Insightify.MVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> News([FromQuery] string? title = null, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 50, [FromQuery] bool json = false)
        {
            var result = await _newsService.GetNews(title, pageIndex, pageSize);


            return json ? Json(result) : View(result);
        }
    }
}

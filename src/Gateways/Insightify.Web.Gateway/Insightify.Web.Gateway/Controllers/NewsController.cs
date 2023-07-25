using Insightify.Web.Gateway.Services.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.Web.Gateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewsController : ControllerBase    
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        [HttpGet]
        public async Task<IActionResult> Articles([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 50)
        {
            var result = await _newsService.GetArticles(pageIndex, pageSize);
            return result != null ? Ok(result) : NotFound();
        }
    }
}

using Insightify.Web.Gateway.Services.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.Web.Gateway.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class NewsController : ControllerBase    
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        [HttpGet]
        public async Task<IActionResult> Test([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 50)
        {
            return Ok(await _newsService.GetArticles(pageIndex, pageSize));
        }
    }
}

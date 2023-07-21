using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.Web.Gateway.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class NewsController : ControllerBase    
    {
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}

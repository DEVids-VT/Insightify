using Microsoft.AspNetCore.Mvc;

namespace Insighify.FinancialDataApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoDataController : ControllerBase
    {
        [HttpGet("{coinId}")]
        public IActionResult GetCryptoData(string coinId, [FromQuery] bool includeDeleted)
        {
            var result = new List<string> { "bitcoin", "etherium" };

            return Ok(result);
        }
    }
}

using Insightify.Web.Gateway.Infrastructure.Enums;
using Insightify.Web.Gateway.Services.FinancialData;
using Insightify.Web.Gateway.Services.News;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.Web.Gateway.Controllers
{
    [ApiController]
    public class FinancialDataController : ControllerBase
    {
        private readonly IFinancialDataService _financialDataService;
        public FinancialDataController(IFinancialDataService financialDataService)
        {
            _financialDataService = financialDataService;
        }
        [HttpGet]
        [Route("/all")]
        public async Task<IActionResult> AllCurrencies()
        {
            var result = await _financialDataService.GetAllCurrencies();
            return result != null ? Ok(result) : NotFound();
        }
        [HttpGet]
        [Route("/{currency}")]
        public async Task<IActionResult> Currency([FromRoute] string currency)
        {
            var hasParsed = Enum.TryParse(currency, true, out CryptoCurrency enumCurrency);
            if (!hasParsed)
            {
                return BadRequest();
            }
            var result = await _financialDataService.GetCurrency(enumCurrency);
            return result != null ? Ok(result) : NotFound();
        }
        [HttpGet]
        [Route("/{currency}/chart")]
        public async Task<IActionResult> Chart([FromRoute] string currency)
        {
            var hasParsed = Enum.TryParse(currency, true, out CryptoCurrency enumCurrency);
            if (!hasParsed)
            {
                return BadRequest();
            }
            var result = await _financialDataService.GetMarketChart(enumCurrency);
            return result != null ? Ok(result) : NotFound();
        }


    }
}

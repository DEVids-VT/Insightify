using Insighify.FinancialDataApi.Infrastructure.Enums;
using Insighify.FinancialDataApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Insighify.FinancialDataApi.Controllers
{
    [ApiController]
    public class CryptoDataController : ControllerBase
    {
        private readonly ICryptoDataService _cryptoDataService;
        public CryptoDataController(ICryptoDataService cryptoDataService)
        {
            _cryptoDataService = cryptoDataService;
        }
        [HttpGet]
        [Route("/{currency}")]
        public async Task<IActionResult> GetCryptoCurrency(string currency)
        {

            var hasParsed = Enum.TryParse(currency, true, out CryptoCurrency enumCurrency);
            if (!hasParsed)
            {
                return BadRequest();
            }
            var coin= await _cryptoDataService.GetCryptoCurrencyAsync(enumCurrency);
            return Ok(coin);
        }
        [HttpGet]
        [Route("/{currency}/chart")]
        public async Task<IActionResult> GetMarketChart(string currency)
        {

            var hasParsed = Enum.TryParse(currency, true, out CryptoCurrency enumCurrency);
            if (!hasParsed)
            {
                return BadRequest();
            }
            var chart = await _cryptoDataService.GetMarketChartAsync(enumCurrency);
            return Ok(chart);
        }
        [HttpGet]
        [Route("/all")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var currencies = await _cryptoDataService.GetAllCurrenciesAsync();
            return Ok(currencies);
        }
    }
}

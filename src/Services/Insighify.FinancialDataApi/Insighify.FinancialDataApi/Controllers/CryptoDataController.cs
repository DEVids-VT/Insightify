using Insighify.FinancialDataApi.Configuration;
using Insighify.FinancialDataApi.ResponceModels.Crypto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Insighify.FinancialDataApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoDataController : ControllerBase
    {
        [HttpGet("/historicalData")]
        public async Task<IActionResult> GetHistoricalData(string id, string currency, string days, string interval, string precision)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(UrlsConfig.CryptoCoinsOpperations.GetHistoricalDataByCoinId(id, currency, days, interval, precision));

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var coinHistory = JsonConvert.DeserializeObject<CoinHistory>(jsonResponse);
                    return Ok(coinHistory);
                }
                else
                {
                    throw new Exception($"Failed to fetch data. Status code: {response.StatusCode}");
                }
            }
        }
    }
}

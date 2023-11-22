using Insightify.MVC.Services.FinancialData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.MVC.Controllers
{
    [Authorize]
    public class FinancialDataController : Controller
    {
        private readonly IFinancialDataService _financialDataService;

        public FinancialDataController(IFinancialDataService financialDataService)
        {
            _financialDataService = financialDataService;
        }

        [HttpGet("marketdata")]
        public async Task<IActionResult> AllCurrencies()
        {
            var model = await _financialDataService.GetAllCurrencies();
            //var dataOut = new List<CryptoCurrencyModel>
            //{
            //    new CryptoCurrencyModel
            //    {
            //        Id = "btc",
            //        Name = "bitcoin",
            //        Image = new Image
            //        {
            //            Small = "/images/camera_lense_0.jpeg"
            //        },
            //        MarketCapRank = 1,
            //        MarketData = new MarketData
            //        {
            //            CurrentPrice = new CurrentPrice
            //            {
            //                Usd = 200
            //            },
            //            MarketCapChange24h = 20,
            //            MarketCap = new MarketCap
            //            {
            //                Usd = 2000
            //            },
            //            TotalVolume = new TotalVolume
            //            {
            //                Usd = 10
            //            },
            //            CirculatingSupply = 200
            //        },
            //        Categories = new List<string>
            //        {
            //            "sdadsaasdadsasdadsasd"
            //        }
            //    }
            //};
            //
            //var model = new Page<CryptoCurrencyModel>(
            //    dataOut,
            //    1,
            //    20,
            //    40);
            //
            return View(model);
        }

        [HttpGet("currency/{currency}")]
        public async Task<IActionResult> Currency(string currency)
        {
            var model = await _financialDataService.Currency(currency);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Chart(string currency)
        {
            var model = await _financialDataService.Chart(currency);

            return Json(model);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var model = await _financialDataService.Dashboard();
            //var model = new Models.FinancialData.DashboardModel
            //{
            //    ChartData = new Web.Gateway.Models.FinancialData.MarketChartModel
            //    {
            //        Prices = new List<MarketValue>
            //        {
            //            new MarketValue { Timestamp = 1355314320000, Value = 129 },
            //            new MarketValue { Timestamp = 1355314620000, Value = 135 },
            //        },
            //        TotalVolumes = new List<MarketValue>
            //        {
            //            new MarketValue { Timestamp = 1355314320000, Value = 129 },
            //            new MarketValue { Timestamp = 1355314620000, Value = 135 },
            //        },
            //        MarketCaps = new List<  MarketValue>
            //        {
            //            new MarketValue { Timestamp = 1355314320000, Value = 129 },
            //            new MarketValue { Timestamp = 1355314620000, Value = 135 },
            //        }
            //
            //    },
            //    Currencies = new List<Models.FinancialData.DashboardCurrencyModel>
            //    {
            //        new Models.FinancialData.DashboardCurrencyModel
            //        {
            //            CurrentPrice = 123,
            //            Image = "",
            //            Name = "bitcoin",
            //            PriceChange = 123
            //        },
            //        new Models.FinancialData.DashboardCurrencyModel
            //        {
            //            CurrentPrice = 123,
            //            Image = "",
            //            Name = "bitcoin",
            //            PriceChange = 123
            //        },
            //        new Models.FinancialData.DashboardCurrencyModel
            //        {
            //            CurrentPrice = 123,
            //            Image = "",
            //            Name = "bitcoin",
            //            PriceChange = 123
            //        },
            //        new Models.FinancialData.DashboardCurrencyModel
            //        {
            //            CurrentPrice = 123,
            //            Image = "",
            //            Name = "bitcoin",
            //            PriceChange = 123
            //        }
            //    }
            //};

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DashboardJson([FromQuery] string currency)
        {
            var model = await _financialDataService.Chart(currency);
            var data = await _financialDataService.Currency(currency);
            return Json(new
            {
                model = model,
                data = data
            });
            //var rand = new Random();
            //return Json(new
            //{
            //    model = new Web.Gateway.Models.FinancialData.MarketChartModel
            //    {
            //        Prices = new List<MarketValue>
            //        {
            //            new MarketValue { Timestamp = 163042560000, Value = rand.Next() },
            //            new MarketValue { Timestamp = 163051200000, Value = rand.Next() },
            //            new MarketValue { Timestamp = 163059840000, Value = rand.Next() },
            //            new MarketValue { Timestamp = 163068480000, Value = rand.Next() },
            //            new MarketValue { Timestamp = 163077120000, Value = rand.Next() }
            //        },                                          
            //        TotalVolumes = new List<MarketValue>        
            //        {                                           
            //            new MarketValue { Timestamp = 163042560000, Value = rand.Next() },
            //            new MarketValue { Timestamp = 163051200000, Value = rand.Next() },
            //            new MarketValue { Timestamp = 163059840000, Value = rand.Next() },
            //            new MarketValue { Timestamp = 163068480000, Value = rand.Next() },
            //            new MarketValue { Timestamp = 163077120000, Value = rand.Next() }
            //        },                                          
            //        MarketCaps = new List<MarketValue>          
            //        {                                           
            //            new MarketValue { Timestamp = 163042560000, Value = rand.Next() },
            //            new MarketValue { Timestamp = 163051200000, Value = rand.Next() },
            //            new MarketValue { Timestamp = 163059840000, Value = rand.Next() },
            //            new MarketValue { Timestamp = 163068480000, Value = rand.Next() },
            //            new MarketValue { Timestamp = 163077120000, Value = rand.Next() }
            //        }
            //
            //    },
            //    data = new Models.FinancialData.DashboardCurrencyModel
            //    {
            //        CurrentPrice = 123,
            //        Image = "",
            //        Name = "bitcoin",
            //        PriceChange = 123
            //    }
            //});
        }
    }
}

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

        [HttpGet]
        public async Task<IActionResult> AllCurrencies()
        {
            var model = await _financialDataService.GetAllCurrencies();

            return View(model);
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            //var model = await _financialDataService.Dashboard();
            var model = new Models.FinancialData.DashboardModel
            {
                ChartData = new Web.Gateway.Models.FinancialData.MarketChartModel
                {
                    Prices = new List<Models.FinancialData.MarketDataModels.MarketValue>
                    {
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630425600, Value = 129 },
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630512000, Value = 135 },
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630598400, Value = 140 },
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630684800, Value = 145 },
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630771200, Value = 150 }
                    },
                    TotalVolumes = new List<Models.FinancialData.MarketDataModels.MarketValue>
                    {
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630425600, Value = 200 },
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630512000, Value = 205 },
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630598400, Value = 210 },
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630684800, Value = 215 },
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630771200, Value = 220 }
                    },
                    MarketCaps = new List<Models.FinancialData.MarketDataModels.MarketValue>
                    {
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630425600, Value = 300 },
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630512000, Value = 310 },
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630598400, Value = 320 },
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630684800, Value = 330 },
                        new Models.FinancialData.MarketDataModels.MarketValue { Timestamp = 1630771200, Value = 340 }
                    }

                },
                Currencies = new List<Models.FinancialData.DashboardCurrencyModel>
                {
                    new Models.FinancialData.DashboardCurrencyModel
                    {
                        CurrentPrice = 123,
                        Image = "",
                        Name = "bitcoin",
                        PriceChange = 123
                    },
                    new Models.FinancialData.DashboardCurrencyModel
                    {
                        CurrentPrice = 123,
                        Image = "",
                        Name = "bitcoin",
                        PriceChange = 123
                    },
                    new Models.FinancialData.DashboardCurrencyModel
                    {
                        CurrentPrice = 123,
                        Image = "",
                        Name = "bitcoin",
                        PriceChange = 123
                    },
                    new Models.FinancialData.DashboardCurrencyModel
                    {
                        CurrentPrice = 123,
                        Image = "",
                        Name = "bitcoin",
                        PriceChange = 123
                    }
                }
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DashboardJson([FromQuery] string currency)
        {
            var model = await _financialDataService.Chart(currency);
            var data = await _financialDataService.Currency(currency);

            return Json(new { model = model, data = data });
        }
    }
}

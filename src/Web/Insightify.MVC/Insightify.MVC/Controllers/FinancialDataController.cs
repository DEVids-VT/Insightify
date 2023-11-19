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
            var model = await _financialDataService.Dashboard();

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

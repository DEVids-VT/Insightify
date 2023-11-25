using Insightify.MVC.Common.LanguagePresets;
using Insightify.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Insightify.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Dashboard", "FinancialData");
            }
            return View(Homepage.English);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(string language)
        {
            var langModel = Homepage.English;

            if (language == Languages.Bg)
            {
                langModel = Homepage.Bulgarian;
            }

            return View(langModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
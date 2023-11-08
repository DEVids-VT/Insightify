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

        public IActionResult Crypto()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
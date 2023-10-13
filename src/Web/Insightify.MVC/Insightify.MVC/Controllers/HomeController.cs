using Insightify.MVC.Common.LanguagePresets;
using Insightify.MVC.Models;
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

        public IActionResult Index()
        {
            var langModel = Homepage.EnglishHomepage;

            return View(langModel);
        }

        [HttpPost]
        public IActionResult Index(string language)
        {
            var langModel = new HomeLanguageModel();

            if (language == "bg")
            {
                langModel = Homepage.BulgarianHomepage;
            }
            else if (language == "en")
            {
                langModel = Homepage.EnglishHomepage;
            }

            return View(langModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
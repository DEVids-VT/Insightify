using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Insightify.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SignIn(string returnUrl)
        {
            var user = User as ClaimsPrincipal;
            var token = await HttpContext.GetTokenAsync("access_token");
            

            _logger.LogInformation("----- User {@User} authenticated into {AppName}", user, Program.AppName);

            if (token != null)
            {
                ViewData["access_token"] = token;
            }
            
            return Ok();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeUsername(string username)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeProfilePicture(IFormFile image)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeEmail(string email)
        {
            return View();
        }
    }
}

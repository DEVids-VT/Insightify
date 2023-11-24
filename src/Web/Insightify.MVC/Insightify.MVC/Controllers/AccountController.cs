using Insightify.MVC.Services;
using Insightify.Web.Gateway.Clients;
using Insightify.Web.Gateway.Clients.Models.Users;
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
        private readonly IAccountClient _accountClient;
        private readonly HttpClient _client;

        public AccountController(ILogger<AccountController> logger, IAccountClient accountClient, HttpClient client)
        {
            _logger = logger;
            _accountClient = accountClient;
            _client = client;
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

            return RedirectToAction("Dashboard", "FinancialData");
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> EditProfile(ApplicationUser user, IFormFile? image)
        {
            if(image != null)
            {
                var url = await UploadImage.ToImgur(image, _client);
                user.ProfilePicture = url;
            }
            user.Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _accountClient.EditProfile(user);

            return Json(result);
        }
    }
}

using Insightify.Web.Gateway.Clients.Models.Users;
using Insightify.Web.Gateway.Services.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.Web.Gateway.Controllers
{
    [ApiController]
    public class AccountSettingsController : ControllerBase
    {
        private readonly IAccoundtSettingsService _settingsService;

        public AccountSettingsController(IAccoundtSettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpPut]
        [Route("/editProfile")]
        public async Task<IActionResult> EditProfile(ApplicationUser user)
        {
            var result = await _settingsService.EditProfile(user);

            return Ok(result);
        }
    }
}

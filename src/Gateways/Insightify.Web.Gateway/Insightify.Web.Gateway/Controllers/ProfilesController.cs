using Insightify.Web.Gateway.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.Web.Gateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfilesClient _profilesClient;

        public ProfilesController(IProfilesClient profilesClient)
        {
            _profilesClient = profilesClient;
        }

        [HttpGet]
        [Route("/profile/{uId}")]
        public async Task<IActionResult> Profile(string uId)
        {
            var user = await _profilesClient.Profile(uId);
            return Ok(user.Content);
        }
    }
}

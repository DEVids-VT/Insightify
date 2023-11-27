using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Insightify.IdentityAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Insightify.IdentityAPI.Configuration
{
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(userId);

            var claims = new List<Claim>
            {
                new Claim("profile_picture", user.ProfilePicture ?? string.Empty),
                new Claim("email", user.Email ?? string.Empty),
                new Claim("username", user.UserName ?? string.Empty),
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(userId);

            context.IsActive = user != null && !await _userManager.IsLockedOutAsync(user);
        }
    }
}

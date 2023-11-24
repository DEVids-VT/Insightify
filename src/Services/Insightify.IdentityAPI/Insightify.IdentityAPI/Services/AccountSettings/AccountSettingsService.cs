using Insightify.IdentityAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace Insightify.IdentityAPI.Services.AccountSettings
{
    public class AccountSettingsService : IAccountSettingsService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountSettingsService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUserEditModel> EditProfile(ApplicationUserEditModel user)
        {
            var foundUser = await _userManager.FindByIdAsync(user.Id);

            if (foundUser != null)
            {
                if (user.ProfilePicture != null)
                {
                    foundUser.ProfilePicture = user.ProfilePicture;
                }

                foundUser.UserName = user.UserName;
                foundUser.Email = user.Email;

                return new ApplicationUserEditModel
                { 
                    Id = foundUser.Id, 
                    ProfilePicture = foundUser.ProfilePicture, 
                    Email = foundUser.Email, 
                    UserName = foundUser.UserName 
                };
            }

            return user;
        }
    }
}

using Insightify.IdentityAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.IdentityAPI.Services.AccountSettings
{
    public interface IAccountSettingsService
    {
        public Task EditProfile(ApplicationUserEditModel user, string uId);
        public Task<UserProfileModel> Profile(string uId);
    }
}

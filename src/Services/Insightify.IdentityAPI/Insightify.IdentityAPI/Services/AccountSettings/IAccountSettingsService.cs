using Insightify.IdentityAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.IdentityAPI.Services.AccountSettings
{
    public interface IAccountSettingsService
    {
        public Task<ApplicationUserEditModel> EditProfile(ApplicationUserEditModel user);
    }
}

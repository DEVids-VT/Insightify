using Insightify.Web.Gateway.Clients.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace Insightify.Web.Gateway.Services.Accounts
{
    public interface IAccoundtSettingsService
    {
        public Task<ApplicationUser> EditProfile(ApplicationUser user);
    }
}

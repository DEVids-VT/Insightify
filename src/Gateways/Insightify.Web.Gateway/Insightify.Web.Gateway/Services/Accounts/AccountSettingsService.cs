using Insightify.Web.Gateway.Clients;
using Insightify.Web.Gateway.Clients.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace Insightify.Web.Gateway.Services.Accounts
{
    public class AccountSettingsService : IAccoundtSettingsService
    {
        private readonly IAccountClient _accountClient;

        public AccountSettingsService(IAccountClient accountClient)
        {
            _accountClient = accountClient;
        }

        public async Task<ApplicationUser> EditProfile(ApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var result = await _accountClient.EditProfile(user);

            return result.Content;
        }
    }
}

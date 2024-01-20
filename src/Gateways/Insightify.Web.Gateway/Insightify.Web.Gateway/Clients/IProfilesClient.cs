using Insightify.Web.Gateway.Clients.Models;
using Insightify.Web.Gateway.Clients.Models.Profiles;
using Refit;

namespace Insightify.Web.Gateway.Clients
{
    public interface IProfilesClient
    {
        [Get("/profile/{uId}")]
        Task<ApiResponse<UserProfileModel>> Profile(string uId);
    }
}

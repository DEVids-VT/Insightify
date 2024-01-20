
using Insightify.MVC.Clients.Models.Profiles;
using Refit;

namespace Insightify.MVC.Clients
{
    public interface IProfilesClient
    {
        [Get("/profile/{uId}")]
        Task<ApiResponse<UserProfileModel>> Profile(string uId);
    }
}

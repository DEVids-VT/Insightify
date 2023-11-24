using Insightify.Web.Gateway.Clients.Models.Posts;
using Insightify.Web.Gateway.Clients.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Insightify.Web.Gateway.Clients
{
    public interface IAccountClient
    {
        [Put("/editProfile")]
        Task<ApiResponse<ApplicationUser>> EditProfile([Body] ApplicationUser user);
    }
}

using Microsoft.AspNetCore.Identity;

namespace Insightify.Web.Gateway.Clients.Models.Users
{
    public class ApplicationUser
    {
        public string Id { get; set; } = default!;
        public string? UserName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Email { get; set; }
    }
}

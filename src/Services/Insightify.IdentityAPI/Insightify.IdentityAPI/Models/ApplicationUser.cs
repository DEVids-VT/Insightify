using Microsoft.AspNetCore.Identity;

namespace Insightify.IdentityAPI.Models
{
    public class ApplicationUser : IdentityUser 
    {
        public string ProfilePicture { get; set; } = default!;
    }
}

namespace Insightify.IdentityAPI.Models
{
    public class ApplicationUserEditModel
    {
        public string? UserName { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string? Email { get; set; }
    }
}

namespace Insightify.IdentityAPI.Models
{
    public class ApplicationUserEditModel
    {
        public string Id { get; set; } = default!;
        public string? UserName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Email { get; set; }
    }
}

namespace Insightify.IdentityAPI.ViewModels
{
    public record LoginInputViewModel
    {
        public string ReturnUrl { get; init; } = null!;

        public string Username { get; init; } = null!;

        public string Password { get; init; } = null!;

        public bool RememberLogin { get; init; }
    }
}

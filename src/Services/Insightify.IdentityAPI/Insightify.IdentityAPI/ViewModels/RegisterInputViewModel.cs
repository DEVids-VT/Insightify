using System.ComponentModel.DataAnnotations;

using Insightify.IdentityAPI.Common;

namespace Insightify.IdentityAPI.ViewModels
{
    public record RegisterInputViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; } = null!;

        [Required]
        [MinLength(ValidationConstants.Register.Validation.UsernameMinLength)]
        [MaxLength(ValidationConstants.Register.Validation.UsernameMaxLength)]
        public string Username { get; init; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(ValidationConstants.Register.Validation.UsernameMinLength)]
        [MaxLength(ValidationConstants.Register.Validation.UsernameMaxLength)]
        public string Password { get; init; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;

        public string ReturnUrl { get; set; } = default!;
    }
}

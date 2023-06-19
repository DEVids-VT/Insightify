using FluentValidation;

using Insightify.IdentityAPI.Common;
using Insightify.IdentityAPI.ViewModels;

namespace Insightify.IdentityAPI.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterInputViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(p => p.Username)
                .NotEmpty();
            RuleFor(p => p.Email)
                .NotEmpty();
            RuleFor(p => p.Password)
                .NotEmpty();
            RuleFor(p => p.Username)
                .MaximumLength(ValidationConstants.Register.Validation.UsernameMaxLength)
                    .WithMessage(string.Format(ValidationConstants.Register.Messages.UsernameMaxLength,
                        ValidationConstants.Register.Validation.UsernameMaxLength))
                .MinimumLength(ValidationConstants.Register.Validation.UsernameMinLength)
                    .WithMessage(string.Format(ValidationConstants.Register.Messages.UsernameMinLength,
                        ValidationConstants.Register.Validation.UsernameMinLength));

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm password is required")
                .Must((registration, confirmPassword) => confirmPassword == registration.Password)
                .WithMessage("Passwords do not match");
        }
    }
}

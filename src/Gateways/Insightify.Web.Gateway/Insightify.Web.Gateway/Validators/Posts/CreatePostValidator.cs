using FluentValidation;
using Insightify.Web.Gateway.Models;
using Insightify.Web.Gateway.Validators.Common;
using static Insightify.Web.Gateway.Validators.Common.PostsValidationConstants.Post;
using static Insightify.Web.Gateway.Validators.Common.PostsValidationConstants.Common;

namespace Insightify.Web.Gateway.Validators.Posts
{
    public class CreatePostValidator : AbstractValidator<CreatePostInputModel>
    {
        public CreatePostValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .MinimumLength(MinTitleLength)
                .MaximumLength(MaxTitleLength);

            RuleFor(p => p.Description)
                .NotEmpty()
                .MinimumLength(MinDescriptionLength)
                .MaximumLength(MaxDescriptionLength);

            RuleFor(p => p.ImageUrl)
                .NotEmpty()
                .MaximumLength(MaxUrlLength);
        }
    }
}

using FluentValidation;
using Insightify.Web.Gateway.Models;
using Insightify.Web.Gateway.Models.Posts;

using static Insightify.Web.Gateway.Validators.Common.PostsValidationConstants.Comment;

namespace Insightify.Web.Gateway.Validators.Posts
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentInputModel>
    {
        public CreateCommentValidator()
        {
            RuleFor(p => p.Content)
                .NotEmpty()
                .MinimumLength(MinContentLength)
                .MaximumLength(MaxContentLength);
        }
    }
}

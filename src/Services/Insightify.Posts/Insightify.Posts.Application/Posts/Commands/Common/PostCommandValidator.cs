using FluentValidation;
using Insightify.Posts.Application.Common;

using static Insightify.Posts.Domain.Posts.ModelConstants.Common;
using static Insightify.Posts.Domain.Posts.ModelConstants.Post;

namespace Insightify.Posts.Application.Posts.Commands.Common
{
    public class PostCommandValidator<TCommand> : AbstractValidator<PostCommand<TCommand>>
        where TCommand : EntityCommand<Guid>
    {
        public PostCommandValidator()
        {
            this.RuleFor(c => c.Title)
                .MinimumLength(MinTitleLength)
                .MaximumLength(MaxTitleLength)
                .NotEmpty();

            this.RuleFor(c => c.Description)
                .MinimumLength(MinDescriptionLength)
                .MaximumLength(MaxDescriptionLength)
                .NotEmpty();

            this.RuleFor(c => c.ImageUrl).Custom((imageUrl, context) =>
            {
                if (imageUrl == null)
                {
                    return;
                }

                if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
                {
                    context.AddFailure("'{PropertyName}' must be a valid url.");
                }
            });
        }
    }
}

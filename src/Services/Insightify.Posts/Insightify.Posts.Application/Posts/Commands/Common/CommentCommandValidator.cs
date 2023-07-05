using FluentValidation;
using Insightify.Posts.Application.Common;
using static Insightify.Posts.Domain.Posts.ModelConstants.Comment;

namespace Insightify.Posts.Application.Posts.Commands.Common
{
    public class CommentCommandValidator<TCommand> : AbstractValidator<CommentCommand<TCommand>>
        where TCommand : EntityCommand<int>
    {
        public CommentCommandValidator()
        {
            this.RuleFor(c => c.Content)
                .MinimumLength(MinContentLength)
                .MaximumLength(MaxContentLength)
                .NotEmpty();
        }
    }
}

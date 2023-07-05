using FluentValidation;
using Insightify.Posts.Application.Posts.Commands.Common;

namespace Insightify.Posts.Application.Posts.Commands.Create
{
    public class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
    {
        public AddCommentCommandValidator() => this.Include(new CommentCommandValidator<AddCommentCommand>());

    }
}

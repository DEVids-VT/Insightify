namespace Insightify.Posts.Application.Posts.Commands.Edit
{
    using FluentValidation;
    using Insightify.Posts.Application.Posts.Commands.Common;

    public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
    {
        public EditPostCommandValidator() => this.Include(new PostCommandValidator<EditPostCommand>());

    }
}

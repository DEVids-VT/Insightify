namespace Insightify.Posts.Application.Posts.Commands.Create
{
    using FluentValidation;
    using Insightify.Posts.Application.Posts.Commands.Common;

    public class EditPostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public EditPostCommandValidator() => this.Include(new PostCommandValidator<CreatePostCommand>());

    }
}

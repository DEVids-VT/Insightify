namespace Insightify.Posts.Application.Posts.Commands.Create
{
    using FluentValidation;
    using Insightify.Posts.Application.Posts.Commands.Common;

    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator() => this.Include(new PostCommandValidator<CreatePostCommand>());

    }
}

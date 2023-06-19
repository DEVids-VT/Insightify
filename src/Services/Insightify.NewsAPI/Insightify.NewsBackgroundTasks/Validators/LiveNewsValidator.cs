using FluentValidation;

using Insightify.NewsBackgroundTasks.Common;
using Insightify.NewsBackgroundTasks.Infrastructure.Models;

namespace Insightify.NewsBackgroundTasks.Validators
{
    public class LiveNewsValidator : AbstractValidator<LiveNewsArticleModel>
    {
        public LiveNewsValidator()
        {
            RuleFor(a => a.Title)
                .NotEmpty();
            RuleFor(a => a.PublishedAt)
                .NotEmpty();
            RuleFor(a => a.Country)
                .NotEmpty();
            RuleFor(a => a.Description)
                .NotEmpty();
            RuleFor(a => a.Url)
                .NotEmpty();
            RuleFor(a => a.Source)
                .NotEmpty();

            RuleFor(p => p.Author)
                .MaximumLength(ValidationConstants.LiveNews.Validation.AuthorMaxLength)
                .WithMessage(ValidationConstants.LiveNews.Messages.AuthorMaxLength);

            RuleFor(p => p.Title)
                .MinimumLength(ValidationConstants.LiveNews.Validation.TitleMinLength)
                .WithMessage(ValidationConstants.LiveNews.Messages.TitleMinLength);

            RuleFor(p => p.Description)
                .MinimumLength(ValidationConstants.LiveNews.Validation.DescriptionMinLength)
                .WithMessage(ValidationConstants.LiveNews.Messages.DescriptionMinLength);
        }
    }
}

using FluentValidation;
using Insightify.NotificationsAPI.Constants;
using Insightify.NotificationsAPI.Models;

namespace Insightify.NotificationsAPI.Validators
{
    public class NotificationValidator : AbstractValidator<Notification>
    {
        public NotificationValidator() 
        {
            RuleFor(p => p.Title)
                .MinimumLength(Validation.Notification.TitleMinLength)
                .MaximumLength(Validation.Notification.TitleMaxLength)
                .NotEmpty();
            RuleFor(p => p.Description)
                .MinimumLength(Validation.Notification.DescriptionMinLength)
                .MaximumLength(Validation.Notification.DescriptionMaxLength)
                .NotEmpty();
            RuleFor(p => p.SentDate)
                .NotEmpty();
        }
    }
}

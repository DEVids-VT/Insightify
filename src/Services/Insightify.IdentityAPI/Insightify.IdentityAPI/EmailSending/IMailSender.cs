namespace Insightify.IdentityAPI.EmailSending
{
    public interface IMailSender
    {
        Task SendEmailAsync(EmailMessage message);
    }
}

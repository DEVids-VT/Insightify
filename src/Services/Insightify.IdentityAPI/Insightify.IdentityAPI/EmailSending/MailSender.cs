using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace Insightify.IdentityAPI.EmailSending
{
    public class MailSender : IMailSender
    {
        private readonly EmailSendingSettings config;
        public MailSender(IOptions<EmailSendingSettings> opts)
        {
            config = opts.Value;
        }
        public async Task SendEmailAsync(EmailMessage message)
        {
            var mimeMessage = new MimeMessage()
            {
                Sender = new MailboxAddress(config.FromName, config.FromAddress),
                Subject = message.Subject,
                Body = new TextPart(TextFormat.Html) { Text = message.Content }
            };
            mimeMessage.To.Add(new MailboxAddress("", message.To));

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(config.SmtpServer, config.SmtpPort, SecureSocketOptions.StartTls);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(config.SmtpUsername, config.SmtpPassword);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}

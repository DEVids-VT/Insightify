namespace Insightify.IdentityAPI.EmailSending
{
    public record EmailSendingSettings
    {
        public string SmtpServer { get; init; } = default!;
        public int SmtpPort { get; init; } = default!;
        public string SmtpUsername { get; init; } = default!;
        public string SmtpPassword { get; init; } = default!;
        public string FromName { get; init; } = default!;
        public string FromAddress { get; init; } = default!;
    }
}

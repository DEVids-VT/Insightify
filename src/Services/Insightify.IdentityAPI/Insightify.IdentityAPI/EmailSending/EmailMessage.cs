namespace Insightify.IdentityAPI.EmailSending
{
    public record EmailMessage
    {
        public string To { get; init; } = default!;
        public string Subject { get; init; } = default!;
        public string Content { get; init; } = default!;
    }
}

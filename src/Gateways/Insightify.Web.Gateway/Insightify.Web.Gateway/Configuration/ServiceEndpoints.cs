namespace Insightify.Web.Gateway.Configuration
{
    public record ServiceEndpoints
    {
        public string News { get; init; }
        public string Posts { get; init; }
        public string FinancialData { get; init; }
        public string Account { get; init; }
    }
}

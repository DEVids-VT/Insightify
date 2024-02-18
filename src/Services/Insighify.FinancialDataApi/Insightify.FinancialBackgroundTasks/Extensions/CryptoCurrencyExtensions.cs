using Insightify.FinancialBackgroundTasks.Infrastructure.Enums;

namespace Insightify.FinancialBackgroundTasks.Extensions
{
    public static class CryptoCurrencyExtensions
    {
        public static string GetID(this CryptoCurrency currency)
        {
            return currency switch
            {
                CryptoCurrency.Cardano => "cardano",
                CryptoCurrency.Tether => "tether",
                CryptoCurrency.Solana => "solana",
                CryptoCurrency.Polkadot => "polkadot",
                CryptoCurrency.BinanceCoin => "binancecoin",
                CryptoCurrency.Bitcoin => "bitcoin",
                CryptoCurrency.Ethereum => "ethereum",
                CryptoCurrency.Ripple => "ripple",
                _ => throw new ArgumentException("Invalid cryptocurrency"),
            };
        }
    }
}

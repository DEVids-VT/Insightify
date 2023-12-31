﻿using Insightify.FinancialBackgroundTasks.Infrastructure.Enums;

namespace Insightify.FinancialBackgroundTasks.Extensions
{
    public static class CryptoCurrencyExtensions
    {
        public static string GetID(this CryptoCurrency currency)
        {
            return currency switch
            {
                CryptoCurrency.Bitcoin => "bitcoin",
                CryptoCurrency.Ethereum => "ethereum",
                CryptoCurrency.Cardano => "cardano",
                CryptoCurrency.BinanceCoin => "binancecoin",
                CryptoCurrency.Tether => "tether",
                CryptoCurrency.Solana => "solana",
                CryptoCurrency.Polkadot => "polkadot",
                _ => throw new ArgumentException("Invalid cryptocurrency"),
            };
        }
    }
}

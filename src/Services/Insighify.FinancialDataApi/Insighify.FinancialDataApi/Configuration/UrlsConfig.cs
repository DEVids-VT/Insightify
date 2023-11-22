namespace Insighify.FinancialDataApi.Configuration
{
    public class UrlsConfig
    {
        public class CryptoCoinsOpperations
        {
            public static string GetCurrentDataByCoinId(string id) => $"/coins/{id}";
            public static string GetCurrentData() => $"/coins/list/";
            public static string GetHistoricalDataByCoinId(string id, string currency, string days, string interval, string precision) 
                => $"/coins/{id}/market_chart?vs_currency={currency}&days={days}&interval={interval}&precision={precision}";
        }
    }
}

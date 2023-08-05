namespace Insighify.FinancialDataApi.Configuration
{
    public class UrlsConfig
    {
        public class CryptoCoinsOpperations
        {
            public static string GetCurrentDataByCoinId(string id) => $"/coins/{id}";
        }
    }
}

using Newtonsoft.Json;

namespace Insightify.Web.Gateway.Infrastructure.Pagination
{
    public static class HeaderHelpers
    {
        public static Dictionary<string, int>? ParseHeader(string? header)
        {
            if (header == null)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Dictionary<string, int>>(header);
        }
    }
}

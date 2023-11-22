using Newtonsoft.Json;

namespace Insightify.MVC.Infrastructure.Pagination
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

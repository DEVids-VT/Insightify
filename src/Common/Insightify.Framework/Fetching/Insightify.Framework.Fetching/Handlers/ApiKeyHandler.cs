using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Insightify.Framework.Fetching.Handlers
{
    public class ApiKeyHandler : DelegatingHandler
    {
        private readonly string _apiKey;
        private readonly string _apiKeyQueryParamName;

        public ApiKeyHandler(string apiKey, string apiKeyQueryParamName)
        {
            _apiKey = apiKey;
            _apiKeyQueryParamName = apiKeyQueryParamName;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Add the API key as a query parameter
            var uriBuilder = new UriBuilder(request.RequestUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[_apiKeyQueryParamName] = _apiKey;
            uriBuilder.Query = query.ToString();
            request.RequestUri = uriBuilder.Uri;

            // Call the inner handler
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

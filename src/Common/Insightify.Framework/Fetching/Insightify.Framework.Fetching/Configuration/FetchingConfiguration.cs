using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.Fetching.Configuration
{
    public class FetchingConfiguration
    {
        public string Name { get; set; } = null!;
        public string? BaseUrl { get; private set; }
        public string? ApiKey { get; private set; }
        public string? ApiKeyQueryParemeter { get; private set; }
        public TimeSpan Timeout { get; private set; } = TimeSpan.FromSeconds(30);
        public int MaxRetries { get; private set; } = 3;
        public bool UsesApiKey => !string.IsNullOrEmpty(ApiKey) && !string.IsNullOrEmpty(ApiKeyQueryParemeter);
        public FetchingConfiguration WithName(string value)
        {
            this.Name = value;
            return this;
        }
        public FetchingConfiguration WithBaseUrl(string? value)
        {
            this.BaseUrl = value;
            return this;
        }
        public FetchingConfiguration WithApiKey(string value, string queryParameterName)
        {
            this.ApiKey = value;
            this.ApiKeyQueryParemeter = queryParameterName;
            return this;
        }

        public FetchingConfiguration WithTimeout(TimeSpan timeout)
        {
            this.Timeout = timeout;
            return this;
        }
        public FetchingConfiguration WithMaxRetries(int maxRetries)
        {
            this.MaxRetries = maxRetries;
            return this;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Insightify.Framework.Fetching.Configuration;
using Insightify.Framework.Fetching.Exceptions;
using Insightify.Framework.Fetching.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;

namespace Insightify.Framework.Fetching
{
    public class ApiFetcher : IApiFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly FetchingConfiguration _config;

        public ApiFetcher(HttpClient httpClient, IOptions<FetchingConfiguration> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
        }
        public async Task<T> FetchDataWithQueryAsync<T>(string endpoint, IDictionary<string, string> queryParams)
        {
            string queryString = string.Join("&", queryParams.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
            return await FetchDataAsync<T>($"{endpoint}?{queryString}");
        }
        public async Task<T> FetchDataAsync<T>(string endpoint)
        {
            var policy = Policy
                .Handle<Exception>(ex => !(ex is JsonSerializationException))
                .WaitAndRetryAsync(_config.MaxRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            return await policy.ExecuteAsync(async () =>
            {
                using var cts = new CancellationTokenSource(_config.Timeout);
                
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint, cts.Token);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    T data = JsonConvert.DeserializeObject<T>(jsonResponse);
                    return data;
                }
                else
                {
                    throw new FetchDataException(response.StatusCode, $"Failed to fetch data from {endpoint}. Status code: {response.StatusCode}");
                }
            });
        }
    }
}

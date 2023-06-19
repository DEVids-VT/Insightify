using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Framework.Fetching.Interfaces
{
    public interface IApiFetcher
    {
        Task<T> FetchDataAsync<T>(string endpoint);
        Task<T> FetchDataWithQueryAsync<T>(string endpoint, IDictionary<string, string> queryParams);
    }
}

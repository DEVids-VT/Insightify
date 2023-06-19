using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.NewsBackgroundTasks.Configuration.Enums;

namespace Insightify.NewsBackgroundTasks.Configuration
{
    public class UrlsConfig
    {
        public class LiveNewsOperations
        {
            public static string GetLiveNews(DateTime date, NewsSort sort, params string[] categories) => $"/v1/news?date={date:yyyy-MM-dd}&sort={sort.ToString()}&categories={string.Join(',', categories)}";
        }
    }
}

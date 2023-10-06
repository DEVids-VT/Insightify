using Insightify.SPA.Clients;
using Insightify.SPA.Models;
using Microsoft.AspNetCore.Components;

namespace Insightify.SPA.Pages
{
    public partial class News
    {
        [Inject] public INewsClient Client { get; set; } = default!;
        public IEnumerable<NewsArticle>? Articles { get; set; } = new List<NewsArticle>();

        protected override async Task OnInitializedAsync()
        {
            
            var articles = await Client.Articles(1, 25);
            if (articles != null)
            {
                Articles = articles.Content;

            }
            await base.OnInitializedAsync();
        }
    }
}

using Insightify.Framework.Pagination;
using Insightify.MVC.Models;
using Insightify.MVC.Services.News;
using Insightify.MVC.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Insightify.MVC.Controllers
{
    [AllowAnonymous]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> News([FromQuery] string? title = null, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 50, [FromQuery] bool json = false)
        {
            //var result = await _newsService.GetNews(title, pageIndex, pageSize);

            await Task.Delay(1000);

            var postViewModelList = new List<NewsViewModel>();
            var random = new Random();
            string[] words = { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew", "indian plum", "jackfruit", "kiwi", "lemon", "mango", "nectarine", "orange", "papaya", "quince", "raspberry", "strawberry", "tangerine", "ugli fruit", "vanilla", "watermelon", "xylocarp", "yellow passionfruit", "zucchini" };

            for (int i = 0; i < 20; i++)
            {
                int wordCount = random.Next(30, 100 + 1);
                var stringBuilder = new StringBuilder();

                for (int j = 0; j < wordCount; j++)
                {
                    int index = random.Next(words.Length);
                    stringBuilder.Append(words[index]);
                    stringBuilder.Append(' ');
                }

                postViewModelList.Add(new NewsViewModel
                {
                    Id = (i + 1).ToString(),
                    Author = (i + 1).ToString(),
                    PublishedAt = DateTime.Now,
                    Country = (i + 1).ToString(),
                    CreatedDateTime = DateTime.Now,
                    IsDeleted = false,
                    RowVersion = 2,
                    Source = (i + 1).ToString(),
                    UpdatedDateTime = DateTime.Now,
                    Url = (i + 1).ToString(),
                    Description = stringBuilder.ToString(),
                    Title = words[random.Next(0, words.Length - 1)],
                    Image = "https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885_1280.jpg"
                });
            }

            var result = new Page<NewsViewModel>(postViewModelList, 1, 10, 20);

            return json ? Json(result) : View(result);
        }
    }
}

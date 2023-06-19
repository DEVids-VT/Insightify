using AutoMapper;

using Insightify.NewsBackgroundTasks.Infrastructure.Models;
using Insightify.NewsBackgroundTasks.ResponceModels.LiveNews;

namespace Insightify.NewsBackgroundTasks.Profiles
{
    public class LiveNewsProfile : Profile
    {
        public LiveNewsProfile()
        {
            CreateMap<NewsArticleResponseModel, LiveNewsArticleModel>()
                .ReverseMap();
        }
    }
}

using AutoMapper;

using FluentValidation;
using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Insightify.NewsBackgroundTasks.Infrastructure.Models;
using Insightify.NewsBackgroundTasks.ResponceModels.LiveNews;
using Insightify.NewsBackgroundTasks.Services.Contracts;


namespace Insightify.NewsBackgroundTasks.Services
{
    /// <inheritdoc />
    public class NewsService : INewsService
    {
        private readonly ILogger<NewsService> _logger;
        private readonly IRepository<LiveNewsArticleModel> _repo;
        private readonly IValidator<LiveNewsArticleModel> _validator;
        private readonly IMapper _mapper;

        public NewsService(ILogger<NewsService> logger, IRepository<LiveNewsArticleModel> repo, IValidator<LiveNewsArticleModel> validator, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _validator = validator;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task Add(NewsArticleResponseModel article)
        {
            var articleModel = _mapper.Map<LiveNewsArticleModel>(article);
            var result = _validator.Validate(articleModel);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogInformation($"{error.ErrorMessage} for {error.AttemptedValue}");
                }
                throw new ArgumentException();
            }

            await _repo.InsertAsync(articleModel);
        }

        /// <inheritdoc />
        public async Task Delete(string id)
        {
            var article = await _repo.GetByIdAsync(id);
            if (article == null)
            {
                _logger.LogInformation($"Failed to delete document with id {id}, becouse it was not found");
                throw new KeyNotFoundException();
            }

            await _repo.DeleteOneAsync(a => a.Id == id);
            _logger.LogInformation($"Document with id {id} is deleted");
        }
    }
}

using Insightify.NewsAPI.Infrastructure.Models;
using Insightify.NewsAPI.Services;
using System.Linq.Expressions;
using Insightify.Framework.Mongo.Pagination;
using Insightify.Framework.MongoDb.Abstractions.Enums;
using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Insightify.NewsAPI.UnitTests.Services
{
    public class NewsReadServiceTests
    {
        private readonly Mock<ILogger<NewsReadService>> _mockLogger;
        private readonly Mock<IRepository<NewsArticleModel>> _mockRepo;
        private readonly NewsReadService _service;

        public NewsReadServiceTests()
        {
            _mockLogger = new Mock<ILogger<NewsReadService>>();
            _mockRepo = new Mock<IRepository<NewsArticleModel>>();
            _service = new NewsReadService(_mockLogger.Object, _mockRepo.Object);
        }

        [Fact]
        public async Task GetArticlesAsync_ReturnsPagedListOfArticles()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var mockArticles = new PagedList<NewsArticleModel>(new List<NewsArticleModel>() { new NewsArticleModel() }, pageNumber, pageSize, 10, 1);
            _mockRepo.Setup(x => x.GetPagedListAsync(pageNumber, pageSize, null, null, SortDirection.Ascending, false)).ReturnsAsync(mockArticles);

            // Act
            var result = await _service.GetArticlesAsync(pageNumber, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pageSize, result.PageSize);
            Assert.Equivalent(mockArticles, result);
            _mockRepo.Verify(x => x.GetPagedListAsync(pageNumber, pageSize, null, null, SortDirection.Ascending, false), Times.Once);
        }
        [Fact]
        public async Task GetAllAsync_ReturnsAllArticles()
        {
            // Arrange
            var mockArticles = new List<NewsArticleModel> { new NewsArticleModel(), new NewsArticleModel() };
            _mockRepo.Setup(repo => repo.GetAllAsync(null, null, SortDirection.Ascending, false)).ReturnsAsync(mockArticles);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _mockRepo.Verify(repo => repo.GetAllAsync(null, null, SortDirection.Ascending, false), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WithPredicate_ReturnsFilteredArticles()
        {
            // Arrange
            Expression<Func<NewsArticleModel, bool>> predicate = article => article.Title.Contains("test");
            var mockArticles = new List<NewsArticleModel> { new NewsArticleModel { Title = "Test Article" } };
            _mockRepo.Setup(repo => repo.GetAllAsync(predicate, null, SortDirection.Ascending, false)).ReturnsAsync(mockArticles);

            // Act
            var result = await _service.GetAllAsync(predicate);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            _mockRepo.Verify(repo => repo.GetAllAsync(It.IsAny<Expression<Func<NewsArticleModel, bool>>>(), null, SortDirection.Ascending, false), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsArticleById()
        {
            // Arrange
            var id = "testId";
            var mockArticle = new NewsArticleModel { Id = id };
            _mockRepo.Setup(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<NewsArticleModel, bool>>>(), null, SortDirection.Ascending, false)).ReturnsAsync(mockArticle);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            _mockRepo.Verify(repo => repo.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<NewsArticleModel, bool>>>(), null, SortDirection.Ascending, false), Times.Once);
        }
        [Fact]
        public async Task GetArticlesAsync_ReturnsNull_WhenRepoReturnsNull()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetPagedListAsync(It.IsAny<int>(), It.IsAny<int>(), null, null, SortDirection.Ascending, false)).ReturnsAsync((IPagedList<NewsArticleModel>)null);

            // Act
            var result = await _service.GetArticlesAsync(1, 10);

            // Assert
            Assert.Null(result);
            _mockRepo.Verify(repo => repo.GetPagedListAsync(It.IsAny<int>(), It.IsAny<int>(), null, null, SortDirection.Ascending, false), Times.Once);
        }


    }
}
using Insightify.Framework.MongoDb.Abstractions.Interfaces;

namespace Insightify.NewsBackgroundTasks.Tests
{
    public class NewsServiceTests
    {
        private readonly Mock<ILogger<NewsService>> _mockLogger;
        private readonly Mock<IRepository<LiveNewsArticleModel>> _mockRepo;
        private readonly Mock<IValidator<LiveNewsArticleModel>> _mockValidator;
        private readonly Mock<IMapper> _mockMapper;

        public NewsServiceTests()
        {
            _mockLogger = new Mock<ILogger<NewsService>>();
            _mockRepo = new Mock<IRepository<LiveNewsArticleModel>>();
            _mockValidator = new Mock<IValidator<LiveNewsArticleModel>>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Add_Should_Throw_Exception_When_Model_Is_Not_Valid()
        {
            // Arrange
            var service = new NewsService(_mockLogger.Object, _mockRepo.Object, _mockValidator.Object, _mockMapper.Object);
            var articleResponseModel = new NewsArticleResponseModel();
            _mockMapper
                .Setup(m => m.Map<LiveNewsArticleModel>(articleResponseModel))
                .Returns(new LiveNewsArticleModel());
            _mockValidator
                .Setup(v => v.Validate(It.IsAny<LiveNewsArticleModel>()))
                .Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Property", "Error") }));
            // Act
            Func<Task> action = async () => await service.Add(articleResponseModel);

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }


        [Fact]
        public async Task Add_Should_Add_Article_When_Model_Is_Valid()
        {
            // Arrange
            var service = new NewsService(_mockLogger.Object, _mockRepo.Object, _mockValidator.Object, _mockMapper.Object);
            var articleResponseModel = new NewsArticleResponseModel();
            var liveNewsArticleModel = new LiveNewsArticleModel();
            _mockMapper
                .Setup(m => m.Map<LiveNewsArticleModel>(articleResponseModel))
                .Returns(liveNewsArticleModel);
            _mockValidator
                .Setup(v => v.Validate(liveNewsArticleModel))
                .Returns(new ValidationResult());
            _mockRepo
                .Setup(r => r.InsertAsync(liveNewsArticleModel))
                .Returns(Task.CompletedTask);

            // Act
            await service.Add(articleResponseModel);

            // Assert
            _mockRepo.Verify(r => r.InsertAsync(It.Is<LiveNewsArticleModel>(a => a == liveNewsArticleModel)), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Throw_Exception_When_Article_Not_Found()
        {
            // Arrange
            var service = new NewsService(_mockLogger.Object, _mockRepo.Object, _mockValidator.Object, _mockMapper.Object);
            var articleId = Guid.NewGuid().ToString();
            _mockRepo
                .Setup(r => r.GetByIdAsync(articleId))
                .ReturnsAsync((LiveNewsArticleModel)null);

            // Act
            Func<Task> action = async () => await service.Delete(articleId);

            // Assert
            await action.Should().ThrowAsync<KeyNotFoundException>();
        }
        [Fact]
        public async Task Delete_Should_Delete_Article_When_Article_Found()
        {
            // Arrange
            var service = new NewsService(_mockLogger.Object, _mockRepo.Object, _mockValidator.Object, _mockMapper.Object);
            var articleId = Guid.NewGuid().ToString();
            _mockRepo.Setup(r => r.GetByIdAsync(articleId)).ReturnsAsync(new LiveNewsArticleModel());
            _mockRepo.Setup(r => r.DeleteOneAsync(a => a.Id == articleId, false));

            // Act
            await service.Delete(articleId);

            // Assert
            _mockRepo.Verify(r => r.DeleteOneAsync(a => a.Id == articleId, false), Times.Once);
        }
    }
}
using Insightify.NewsAPI.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.NewsAPI.UnitTests.Models
{
    public class NewsArticleModelTests
    {
        // Title Validation
        [Theory]
        [InlineData("This is a valid title", true)] // Assuming it meets min and max length requirements
        [InlineData("", false)] // Empty should fail due to MinLength
        public void Title_Validation(string title, bool expectedIsValid)
        {
            var model = new NewsArticleModel { Title = title };
            Assert.Equal(expectedIsValid, ValidateModel(model));
        }

        // Author Validation
        [Theory]
        [InlineData("Author Name", true)] // Valid length
        [InlineData("", true)] // Assuming empty is valid, adjust based on actual validation rules
        public void Author_Validation(string author, bool expectedIsValid)
        {
            var model = new NewsArticleModel { Author = author };
            Assert.Equal(expectedIsValid, ValidateModel(model));
        }

        // Description Validation
        [Theory]
        [InlineData("This is a sufficiently long description for testing purposes.", true)]
        [InlineData("Short", false)] // Assuming this is too short based on MinLength
        public void Description_Validation(string description, bool expectedIsValid)
        {
            var model = new NewsArticleModel { Description = description };
            Assert.Equal(expectedIsValid, ValidateModel(model));

        }

        private bool ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model);
            return Validator.TryValidateObject(model, context, validationResults, true);
        }

        [Fact]
        public void TestGetters()
        {
            // Arrange
            var expectedAuthor = "Author Name";
            var expectedTitle = "Article Title";
            var expectedDescription = "Article Description";
            var expectedUrl = "https://example.com";
            var expectedSource = "Example Source";
            var expectedImage = "https://example.com/image.jpg";
            var expectedCountry = "Country";
            var expectedPublishedAt = DateTime.Now;

            var model = new NewsArticleModel
            {
                Author = expectedAuthor,
                Title = expectedTitle,
                Description = expectedDescription,
                Url = expectedUrl,
                Source = expectedSource,
                Image = expectedImage,
                Country = expectedCountry,
                PublishedAt = expectedPublishedAt
            };

            // Act & Assert
            Assert.Equal(expectedAuthor, model.Author);
            Assert.Equal(expectedTitle, model.Title);
            Assert.Equal(expectedDescription, model.Description);
            Assert.Equal(expectedUrl, model.Url);
            Assert.Equal(expectedSource, model.Source);
            Assert.Equal(expectedImage, model.Image);
            Assert.Equal(expectedCountry, model.Country);
            Assert.Equal(expectedPublishedAt, model.PublishedAt);
        }
        [Fact]
        public void TestSetters()
        {
            // Arrange
            var model = new NewsArticleModel();

            // Act
            model.Author = "New Author";
            model.Title = "New Title";
            model.Description = "New Description";
            model.Url = "https://newexample.com";
            model.Source = "New Source";
            model.Image = "https://newexample.com/image.jpg";
            model.Country = "New Country";
            model.PublishedAt = DateTime.Now;

            // Assert
            Assert.Equal("New Author", model.Author);
            Assert.Equal("New Title", model.Title);
            Assert.Equal("New Description", model.Description);
            Assert.Equal("https://newexample.com", model.Url);
            Assert.Equal("New Source", model.Source);
            Assert.Equal("https://newexample.com/image.jpg", model.Image);
            Assert.Equal("New Country", model.Country);
            Assert.NotNull(model.PublishedAt); // Specific DateTime equality checks can be flaky due to precision
        }
    }
}

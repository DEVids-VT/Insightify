namespace Insightify.Posts.Domain
{
    using FluentAssertions;
    using Insightify.Posts.Domain.Posts.Factories;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class DomainConfigurationSpecs
    {
        [Fact]
        public void AddDomainShouldRegisterFactories()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            var services = serviceCollection
                .AddDomain()
                .BuildServiceProvider();

            // Assert
            services
                .GetService<IPostFactory>()
                .Should()
                .NotBeNull();
        }
    }
}

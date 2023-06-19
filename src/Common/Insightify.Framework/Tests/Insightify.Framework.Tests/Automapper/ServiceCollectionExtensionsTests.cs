using AutoMapper;
using Insightify.Framework.Automapper;
using Microsoft.Extensions.DependencyInjection;

namespace Insightify.Framework.Tests.Automapper
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void When_AddAutomapper_Should_AddToServices()
        {
            var services = new ServiceCollection();
            var coreServicesBuilder = new CoreServicesBuilder(services);

            var thisAssembly = typeof(ServiceCollectionExtensionsTests).Assembly;

            coreServicesBuilder.AddAutomapper(thisAssembly);

            (services.BuildServiceProvider().GetService<IMapper>() != null).Should().BeTrue();
        }

    }
}
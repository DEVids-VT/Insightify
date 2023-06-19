using FluentAssertions;
using Insigghtify.Framework.Mongo.Extensions;
using Insightify.Framework.MongoDb.Abstractions.Configuration;
using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;

namespace Insightify.Framework.Mongo.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        private ServiceCollection _services;

        public ServiceCollectionExtensionsTests()
        {
            _services = new ServiceCollection();
        }

        [Fact]
        public void AddMongoDatabase_AddsExpectedServices()
        {
            // Arrange
            var coreServicesBuilder = new CoreServicesBuilder(_services);

            // Act
            coreServicesBuilder.AddMongoDatabase(c =>
            {
                c.WithConnectionString("mongodb://localhost:27017").WithDatabaseName("test");
            });

            // Assert
            var _provider = coreServicesBuilder.Services.BuildServiceProvider();

            // Assert
            var mongoClientService = _provider.GetService<IMongoClient>();
            var mongoDatabaseService = _provider.GetService(typeof(IMongoDatabase));

            mongoClientService.Should().NotBeNull();
            mongoDatabaseService.Should().NotBeNull();
        }
    }
}
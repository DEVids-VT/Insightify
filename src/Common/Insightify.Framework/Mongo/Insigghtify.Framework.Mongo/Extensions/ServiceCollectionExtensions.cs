using Insightify.Framework;
using Insightify.Framework.MongoDb.Abstractions;
using Insightify.Framework.MongoDb.Abstractions.Configuration;
using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Insigghtify.Framework.Mongo.Extensions
{
    /// <summary>
    /// Extension methods for setting up Mongo Database in an <see cref="IServiceCollection" />
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static CoreServicesBuilder AddMongoDatabase(this CoreServicesBuilder builder, Action<MongoConfiguration> configuration)
        {
            builder.Services.AddMongoDatabase(configuration);
            return builder;
        }

        public static IServiceCollection AddMongoDatabase(this IServiceCollection services,
            Action<MongoConfiguration> configuration)
        {
            var mongoConfig = new MongoConfiguration();
            configuration(mongoConfig);

            IConvention ignoreIfDefaultOrNullConvention = mongoConfig.IgnoreIfDefaultConvention
                ? new IgnoreIfDefaultConvention(true)
                : new IgnoreIfNullConvention(mongoConfig.IgnoreIfNullConvention);

            var conventionPack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(mongoConfig.EnumConvention),
                ignoreIfDefaultOrNullConvention,
                new IgnoreExtraElementsConvention(true)
            };

            ConventionRegistry.Register("conventionPack", conventionPack, t => true);

            var settings = MongoClientSettings.FromUrl(new MongoUrl(mongoConfig.ConnectionString));

            var client = new MongoClient(settings);
            var database = client.GetDatabase(mongoConfig.Database);

            services.AddSingleton(database);
            services.AddSingleton(typeof(IMongoClient), p => client);
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

            services.Configure<MongoConfiguration>(configuration);

            BsonClassMap.RegisterClassMap<MongoEntity>(p =>
            {
                p.AutoMap();
                p.SetIgnoreExtraElements(true);
            });

            return services;
        }
    }
}

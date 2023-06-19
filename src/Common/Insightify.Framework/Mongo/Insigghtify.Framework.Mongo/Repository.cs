using Insightify.Framework.Mongo.Extensions;
using Insightify.Framework.MongoDb.Abstractions.Configuration;
using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq.Expressions;
using Insightify.Framework.Mongo.Pagination;
using SortDirection = Insightify.Framework.MongoDb.Abstractions.Enums.SortDirection;
using System;
using IdentityModel.OidcClient;

namespace Insigghtify.Framework.Mongo
{
    public class Repository<T> : IRepository<T> where T : class, IMongoEntity 
    {
        private const string DATA = "data";


        private readonly ILogger<Repository<T>> Logger;
        protected MongoConfiguration Configuration { get; set; }
        protected IMongoCollection<T> Collection { get; set; }
        protected IMongoDatabase Database { get; set; }
        protected IMongoClient Client { get; set; }

        public Repository(IMongoClient client, IOptions<MongoConfiguration> configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration.Value;
            if (string.IsNullOrEmpty(Configuration.Database))
            {
                throw new ArgumentNullException($"Database Connection String");
            }
            Logger = loggerFactory.CreateLogger<Repository<T>>();
            Client = client;
            Database = Client.GetDatabase($"{Configuration.Database.ToLower()}");
            Collection = Database.GetCollection<T>(MongoEntityExtensions.GetCollectionName<T>());
        }

        public async Task InsertAsync(T entity)
        {
            entity.RowVersion = 1;
            entity.CreatedDateTime = DateTime.UtcNow;

            await Collection.InsertOneAsync(entity);

            Logger.LogDebug($"MongoDb Insert -> EntityId: {entity.Id}");
        }

        public async Task InsertAsync(ICollection<T>? entities)
        {
            if (entities == null)
            {
                return;
            }

            foreach (var entity in entities)
            {
                entity.RowVersion = 1;
                entity.CreatedDateTime = DateTime.UtcNow;
            }
            await Collection.InsertManyAsync(entities);
            Logger.LogDebug($"MongoDb Insert -> EntityId-s: {string.Join(", ", entities.Select(e => e.Id))}");


        }

        public async Task UpdateAsync(T entity)
        {
            entity.RowVersion += 1;
            entity.UpdatedDateTime = DateTime.UtcNow;
            await Collection.FindOneAndReplaceAsync(Builders<T>.Filter.Eq(p => p.Id, entity.Id), entity);

            Logger.LogDebug($"MongoDb Update -> EntityId: {entity.Id}");

        }

        public async Task DeleteByIdAsync(string id, bool hardDelete = false)
        {
            await DeleteOneAsync(x => x.Id == id, hardDelete);
        }

        public async Task DeleteAsync(T entity, bool hardDelete = false)
        {
            await DeleteOneAsync(x => x.Id == entity.Id, hardDelete);
        }

        public async Task DeleteAsync(ICollection<T> entities, bool hardDelete = false)
        {
            var ids = entities.Select(x => x.Id).ToList();
            await DeleteManyAsync(x => ids.Contains(x.Id));
        }

        public async Task DeleteOneAsync(Expression<Func<T, bool>> predicate, bool hardDelete = false)
        {
            if (Configuration.SoftDeleteConfiguration.IsEnabled && hardDelete == false)
            {
                var filter = Builders<T>.Filter.And(
                    Builders<T>.Filter.Eq("DeletedDateTime", BsonNull.Value), // Check if DeletedDateTime is null
                    Builders<T>.Filter.Where(predicate));

                var update = Builders<T>.Update.Set("DeletedDateTime", DateTime.Now);

                var options = new FindOneAndUpdateOptions<T>
                {
                    ReturnDocument = ReturnDocument.After
                };

                var result = await Collection.FindOneAndUpdateAsync(filter, update, options);
                if (result != null)
                {
                    Logger.LogDebug($"MongoDb SoftDeleted -> EntityId: {result.Id}");
                }

            }
            else
            {
                var result = await Collection.FindOneAndDeleteAsync(predicate);
                if (result != null)
                {
                    Logger.LogDebug($"MongoDb HardDeleted -> EntityId: {result.Id}");
                }
            }
        }

        public async Task DeleteManyAsync(Expression<Func<T, bool>> predicate, bool hardDelete = false)
        {
            if (Configuration.SoftDeleteConfiguration.IsEnabled && hardDelete == false)
            {
                using var session = await Client.StartSessionAsync(new ClientSessionOptions()
                {
                    Snapshot = false
                });
                session.StartTransaction();
                try
                {
                    var entities = await GetAllAsync(predicate, includeDeleted: false);

                    var update = Builders<T>.Update.Set("DeletedDateTime", DateTime.Now);

                    var filter = Builders<T>.Filter.And(
                        Builders<T>.Filter.Eq("DeletedDateTime", BsonNull.Value), // Check if DeletedDateTime is null
                        Builders<T>.Filter.Where(predicate));

                    var updateResult = await Collection.UpdateManyAsync(session, filter, update);


                    if (updateResult.ModifiedCount == entities.Count)
                    {
                        await session.CommitTransactionAsync();
                        Logger.LogDebug($"MongoDb SoftDeleted -> EntityId-s: {string.Join(", ", entities.Select(e => e.Id))}");
                    }
                    else
                    {
                        Logger.LogError("Error while soft DeleteMany. Deleted count does not match entities count. Transaction aborted.");
                        await session.AbortTransactionAsync();
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError("Error writing to MongoDb: " + e.Message);
                    await session.AbortTransactionAsync();
                }
            }
            else
            {
                using var session = await Client.StartSessionAsync();
                session.StartTransaction();
                try
                {
                    var entities = await GetAllAsync(predicate, includeDeleted: false);

                    var deleteResult = await Collection.DeleteManyAsync(predicate);
                    if (deleteResult.DeletedCount == entities.Count)
                    {
                        await session.CommitTransactionAsync();
                        Logger.LogDebug($"MongoDb HardDeleted -> EntityId-s: {string.Join(", ", entities.Select(e => e.Id))}");
                    }
                    else
                    {
                        Logger.LogError("Error while hard DeleteMany. Deleted count does not match entities count. Transaction aborted.");
                        await session.AbortTransactionAsync();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public async Task RestoreAsync(T entity)
        {
            await RestoreByIdAsync(entity.Id);
        }

        public async  Task RestoreByIdAsync(string id)
        {
            var update = Builders<T>.Update.Set(x => x.DeletedDateTime, null);

            var filter = Builders<T>.Filter.And(
                Builders<T>.Filter.Where(x =>
                    x.DeletedDateTime != null && x.Id == id));

            var options = new FindOneAndUpdateOptions<T>
            {
                ReturnDocument = ReturnDocument.After
            };

            var restoreResult = await Collection.FindOneAndUpdateAsync(filter, update, options);
            if (restoreResult != null)
            {
                Logger.LogDebug($"MongoDb Restored -> EntityId: {restoreResult.Id}");
            }
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return await Collection.CountDocumentsAsync(BuildPredicateAsFilterDefinition(predicate));
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var entity = await GetFirstOrDefaultAsync(predicate);
            return entity != null;
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await GetFirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null,
            SortDirection sortDirection = SortDirection.Ascending, bool includeDeleted = false)
        {
            var orderByFilter = BuildOrderByExpressionAsFilterDefinition(orderBy, sortDirection);
            var predicateFilter = BuildPredicateAsFilterDefinition(predicate);
            var softDeleteFilter = BuildSoftDeleteFilterDefinition(includeDeleted);

            var dataFacetValue = new[]
            {
                PipelineStageDefinitionBuilder.Match(softDeleteFilter),
                PipelineStageDefinitionBuilder.Match(predicateFilter),
                PipelineStageDefinitionBuilder.Sort(orderByFilter),

            };
            var dataFacet = AggregateFacet.Create(DATA, PipelineDefinition<T, T>.Create(dataFacetValue));
            var aggregation = await Collection.Aggregate().Facet(dataFacet).ToListAsync();
            Logger.LogDebug($"MongoDb -> GetAll Count: {aggregation.Count}");
            return aggregation.First().Facets.First(x => x.Name == DATA).Output<T>().ToList();
        }

        public async Task<IPagedList<T>> GetPagedListAsync(int pageIndex = 1, int pageSize = 50, Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null,
            SortDirection sortDirection = SortDirection.Ascending, bool includeDeleted = false)
        {
            pageIndex -= 1;
            if (pageIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex));
            }
            var orderByFilter = BuildOrderByExpressionAsFilterDefinition(orderBy, sortDirection);
            var predicateFilter = BuildPredicateAsFilterDefinition(predicate);
            var softDeleteFilter = BuildSoftDeleteFilterDefinition(includeDeleted);

            var filter = softDeleteFilter & predicateFilter;

            var totalCount = await Collection.CountDocumentsAsync(filter);
            var result = await Collection
                .Find(filter)
                .Sort(orderByFilter)
                .Skip(pageIndex * pageSize)
                .Limit(pageSize)
                .ToListAsync();
            var totalPages = 0;
            if (totalCount > 0)
            {
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            }
            Logger.LogDebug($"MongoDb -> GetPagedList Count: {totalCount}");
            return new PagedList<T>(result, pageIndex + 1, pageSize, totalPages, totalCount);
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null,
            SortDirection sortDirection = SortDirection.Ascending, bool includeDeleted = false)
        {
            var orderByFilter = BuildOrderByExpressionAsFilterDefinition(orderBy, sortDirection);
            var predicateFilter = BuildPredicateAsFilterDefinition(predicate);
            var softDeleteFilter = BuildSoftDeleteFilterDefinition(includeDeleted);

            var dataFacetValue = new[]
            {
                PipelineStageDefinitionBuilder.Match(softDeleteFilter),
                PipelineStageDefinitionBuilder.Match(predicateFilter),
                PipelineStageDefinitionBuilder.Sort(orderByFilter),

            };
            var dataFacet = AggregateFacet.Create(DATA, PipelineDefinition<T, T>.Create(dataFacetValue));
            var aggregation = await Collection.Aggregate().Match(predicateFilter).Facet(dataFacet).FirstOrDefaultAsync();

            return aggregation.Facets.First(x => x.Name == DATA).Output<T>().FirstOrDefault();

        }

        public async Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await (await Collection.FindAsync(BuildPredicateAsFilterDefinition(predicate)))
                .SingleOrDefaultAsync();
        }

        #region Helpers

        private SortDefinition<T> BuildOrderByExpressionAsFilterDefinition(Expression<Func<T, object>>? orderBy,
            SortDirection direction)
        {
            if (orderBy == null)
            {
                if (direction == SortDirection.Ascending)
                {
                    var filter = Builders<T>.Sort.Ascending(p => p.CreatedDateTime);
                    return filter;
                }
                else
                {
                    var filter = Builders<T>.Sort.Descending(p => p.CreatedDateTime);
                    return filter;
                }
            }
            if (direction == SortDirection.Ascending)
            {
                var filter = Builders<T>.Sort.Ascending(orderBy);
                return filter;
            }
            else
            {
                var filter = Builders<T>.Sort.Descending(orderBy);
                return filter;
            }
        }
        private FilterDefinition<T> BuildSoftDeleteFilterDefinition(bool includeDeleted)
        {
            var filter = Builders<T>.Filter.Empty;

            if (includeDeleted == false)
            {
                filter = Builders<T>.Filter.Where(p => p.DeletedDateTime == null);
            }

            return filter;
        }
        private FilterDefinition<T> BuildPredicateAsFilterDefinition(Expression<Func<T, bool>>? predicate)
        {
            var filter = Builders<T>.Filter.Empty;

            if (predicate != null)
            {
                filter &= Builders<T>.Filter.Where(predicate);
            }

            return filter;
        }


        #endregion
    }
}

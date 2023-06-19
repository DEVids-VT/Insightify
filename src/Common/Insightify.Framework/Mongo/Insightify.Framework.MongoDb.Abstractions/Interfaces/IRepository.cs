namespace Insightify.Framework.MongoDb.Abstractions.Interfaces
{
    using Insightify.Framework.MongoDb.Abstractions.Enums;
    using System;
    using System.Linq.Expressions;
    public interface IRepository { }

    public interface IRepository<T> : IRepository where T : class, IMongoEntity
    {
        /// <summary>
        /// Inserts a single entity into the repository.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// Inserts a collection of entities into the repository.
        /// </summary>
        /// <param name="entities">The collection of entities to insert.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task InsertAsync(ICollection<T> entities);

        /// <summary>
        /// Updates a single entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        /// <param name="hardDelete">If true, the entity will be permanently deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteByIdAsync(string id, bool hardDelete = false);

        /// <summary>
        /// Deletes a single entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="hardDelete">If true, the entity will be permanently deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(T entity, bool hardDelete = false);

        /// <summary>
        /// Deletes a collection of entities from the repository.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        /// <param name="hardDelete">If true, the entities will be permanently deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(ICollection<T> entities, bool hardDelete = false);

        /// <summary>
        /// Deletes one entity from the repository that satisfies the given predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="hardDelete">If true, the entity will be permanently deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteOneAsync(Expression<Func<T, bool>> predicate, bool hardDelete = false);

        /// <summary>
        /// Deletes many entities from the repository that satisfy the given predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="hardDelete">If true, the entities will be permanently deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteManyAsync(Expression<Func<T, bool>> predicate, bool hardDelete = false);

        /// <summary>
        /// Restores a single entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to restore.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RestoreAsync(T entity);

        /// <summary>
        /// Restores an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to restore.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RestoreByIdAsync(string id);

        /// <summary>
        /// Counts the number of entities in the repository that satisfy the given predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The number of entities that satisfy the condition specified by the predicate.</returns>
        Task<long> CountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Checks if any entity in the repository satisfies the given predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>True if at least one entity satisfies the condition specified by the predicate, otherwise false.</returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>The entity with the specified identifier, or null if no such entity exists.</returns>
        Task<T?> GetByIdAsync(string id);

        /// <summary>
        /// Gets a  list of entities from the repository that satisfy the given predicate, ordered by a specified field.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="sortDirection">The direction of the sort (ascending or descending).</param>
        /// <param name="includeDeleted">If true, the method will include entities that are marked as deleted.</param>
        /// <returns>A  list of entities that satisfy the condition specified by the predicate, ordered by the specified field.</returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null, SortDirection sortDirection = SortDirection.Ascending, bool includeDeleted = false);

        /// <summary>
        /// Gets a paged list of entities from the repository that satisfy the given predicate, ordered by a specified field.
        /// </summary>
        /// <param name="pageIndex">The index of the page to retrieve.</param>
        /// <param name="pageSize">The size of the page to retrieve.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="sortDirection">The direction of the sort (ascending or descending).</param>
        /// <param name="includeDeleted">If true, the method will include entities that are marked as deleted.</param>
        /// <returns>A paged list of entities that satisfy the condition specified by the predicate, ordered by the specified field.</returns>
        Task<IPagedList<T>> GetPagedListAsync(int pageIndex = 1, int pageSize = 50, Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null, SortDirection sortDirection = SortDirection.Ascending, bool includeDeleted = false);

        /// <summary>
        /// Retrieves the first entity that satisfies the given predicate, or default if no such entity exists.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="sortDirection">The direction of the sort (ascending or descending).</param>
        /// <param name="includeDeleted">If true, the method will include entities that are marked as deleted.</param>
        /// <returns>The first entity that satisfies the condition specified by the predicate, or default if no such entity exists.</returns>
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null, SortDirection sortDirection = SortDirection.Ascending, bool includeDeleted = false);

        /// <summary>
        /// Retrieves the single entity that satisfies the given predicate, or default if no such entity exists. Throws an exception if more than one entity satisfies the predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The single entity that satisfies the condition specified by the predicate, or default if no such entity exists. Throws an exception if more than one entity satisfies the predicate.</returns>
        Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

    }
}

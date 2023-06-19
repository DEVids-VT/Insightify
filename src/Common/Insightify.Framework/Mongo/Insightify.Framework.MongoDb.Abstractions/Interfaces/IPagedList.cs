namespace Insightify.Framework.MongoDb.Abstractions.Interfaces
{
    /// <summary>
    /// Provides the interface(s) for paged list of any type
    /// </summary>
    /// <typeparam name="T">The type for paging.</typeparam>

    public interface IPagedList<T>
    {
        /// <summary>
        /// Gets the Page Index
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Gets the Page Size
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Gets the Total Count of the list of <typeparamref name="T" />
        /// </summary>
        long TotalCount { get; }

        /// <summary>
        /// Gets the Total Pages
        /// </summary>
        long TotalPages { get; }

        /// <summary>
        /// Gets a value indicating whether the paged list has a previous page
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Gets a value indicating whether the paged list has a next page
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        /// Gets the Current Page Items
        /// </summary>
        ICollection<T> Items { get; }
    }
}

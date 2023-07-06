using Insightify.Framework.MongoDb.Abstractions.Interfaces;

namespace Insightify.Posts.Infrastructure.Pagination
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IPagedList{T}" /> interface
    /// </summary>
    /// <typeparam name="T">The type of the data to page</typeparam>
    public class PagedList<T> : IPagedList<T>
    {
        /// <inheritdoc />
        public int PageIndex { get; }

        /// <inheritdoc />
        public int PageSize { get; }

        /// <inheritdoc />
        public long TotalCount { get; }

        /// <inheritdoc />
        public long TotalPages { get; }

        /// <inheritdoc />
        public bool HasPreviousPage => this.PageIndex - 1 > 0;

        /// <inheritdoc />
        public bool HasNextPage => this.PageIndex < this.TotalPages;

        /// <inheritdoc />
        public ICollection<T> Items { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
        /// </summary>
        /// <param name="source">The Source Collection</param>
        /// <param name="pageIndex">The Page Number</param>
        /// <param name="pageSize">The Page Size</param>
        /// <param name="totalPages">The Total Pages</param>
        /// <param name="totalCount">The Total Item Count</param>
        public PagedList(ICollection<T> source, int pageIndex, int pageSize, long totalPages, long totalCount)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = totalCount;
            this.TotalPages = totalPages;
            this.Items = source.ToList();
        }
    }
}

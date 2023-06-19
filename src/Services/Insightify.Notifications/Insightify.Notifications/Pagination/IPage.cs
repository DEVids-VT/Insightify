namespace Insightify.NotificationsAPI.Pagination
{
    public interface IPage<T> : IPage, IEnumerable<T>
    {

    }

    public interface IPage
    {
        public int CurrentPage { get; }

        public int PageSize { get; }

        public int TotalCount { get; }
    }
}

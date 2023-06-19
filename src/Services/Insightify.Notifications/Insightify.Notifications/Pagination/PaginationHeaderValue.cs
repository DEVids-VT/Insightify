namespace Insightify.NotificationsAPI.Pagination
{
    public class PaginationHeaderValue
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }
    }
}

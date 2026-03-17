namespace arise_api.helpers
{
    public class PaginationHelper
    {
        public static int GetTotalPages(int totalItems, int pageSize)
        {
            return (int)Math.Ceiling((double)totalItems / pageSize);
        }

        public static int GetCurrentPage(int skip, int take)
        {
            return (skip / take) + 1;
        }
    }
}

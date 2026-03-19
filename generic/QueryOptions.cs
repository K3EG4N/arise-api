using System.Linq.Expressions;

namespace arise_api.generic
{
    public class QueryOptions<T> where T : class
    {
        public Expression<Func<T, bool>>? Predicate { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; set; }
        public Func<IQueryable<T>, IQueryable<T>>? Include { get; set; }
        public int Limit { get; set; } = 0;
        public int Offset { get; set; } = 0;
    }
}

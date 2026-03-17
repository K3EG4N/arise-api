namespace arise_api.generic
{
    public class DataGroup<T>
    {
        public List<T> Data { get; set; } = null!;
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}

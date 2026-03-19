namespace arise_api.generic
{
    public class BaseFilter
    {
        public string Query { get; set; } = string.Empty;

        public int Limit { get; set; } = 50;

        public int Offset { get; set; } = 0;
    }
}

namespace arise_api.dtos.Generics
{
    public class BaseResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        public string Exception { get; set; } = string.Empty;
    }
}

namespace arise_api.dtos.Responses
{
    public class UserByIdResponse
    {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}

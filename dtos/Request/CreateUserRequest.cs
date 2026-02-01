namespace arise_api.dtos.Request
{
    public class CreateUserRequest
    {
        public string? Username { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

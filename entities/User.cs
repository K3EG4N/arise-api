using arise_api.generic;

namespace arise_api.Entities
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string? Username { get; set; }

        public string Password { get; set; } = string.Empty;
    }
}

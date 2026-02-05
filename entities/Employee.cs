using arise_api.Entities;
using arise_api.generic;

namespace arise_api.entities
{
    public class Employee : BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string PaternalLastName { get; set; } = string.Empty;

        public string? MaternalLastName { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Photo { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; } = new();
    }
}

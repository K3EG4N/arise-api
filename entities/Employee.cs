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

        public Guid? UserId { get; set; }

        public User User { get; set; } = new();

        public string Dni { get; set; } = string.Empty;

        public Gender Gender { get; set; } = new();

        public string Code { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public Guid StatusId { get; set; } = new();

        public EmployeeStatus Status { get; set; } = new();
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}

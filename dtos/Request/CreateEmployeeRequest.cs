using arise_api.dtos.Generics;
using arise_api.entities;

namespace arise_api.dtos.Request
{
    public class CreateEmployeeRequest
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Dni { get; set; }
        public string? Phone { get; set; }
        public Gender? Gender { get; set; }
        public string? BirthDate { get; set; }
        public string? Address { get; set; }
        public Guid? DepartmentId { get; set; }
        public FileUpload? File { get; set; }
    }
}

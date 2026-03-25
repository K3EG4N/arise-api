using arise_api.entities;

namespace arise_api.dtos.Request
{
    public class CreateEmployeeRequest
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public Gender Gender { get; set; }
        public string BirthDate { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}

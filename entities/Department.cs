using arise_api.generic;

namespace arise_api.entities
{
    public class Department : BaseEntity
    {
        public Guid DepartmentId { get; set; }
        
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}

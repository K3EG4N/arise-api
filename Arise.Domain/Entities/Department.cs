using Arise.Domain.Common;

namespace Arise.Domain.Entities
{
    public class Department : BaseEntity
    {
        public Guid DepartmentId { get; set; }

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}

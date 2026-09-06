using Arise.Domain.Common;

namespace Arise.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public Guid MenuId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}

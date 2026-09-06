using Arise.Domain.Common;

namespace Arise.Domain.Entities
{
    public class MenuItem : BaseEntity
    {
        public Guid MenuItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int Order { get; set; }
        public string Icon { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
        public MenuItem? Parent { get; set; }
        public Guid MenuId { get; set; }
        public Menu? Menu { get; set; }
    }
}

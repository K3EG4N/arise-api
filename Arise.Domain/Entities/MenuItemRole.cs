using System.Data;

namespace Arise.Domain.Entities
{
    public class MenuItemRole
    {
        public Guid MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
    }
}

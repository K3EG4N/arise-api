using Arise.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arise.Infrastructure.Persistence.Configuration
{
    public class MenuItemRoleConfiguration : IEntityTypeConfiguration<MenuItemRole>
    {
        public void Configure(EntityTypeBuilder<MenuItemRole> builder)
        {
            builder.ToTable("menuItemRoles", schema: "mnu");

            builder.HasKey(x => new { x.MenuItemId, x.RoleId });

            builder.HasOne(x => x.MenuItem)
                   .WithMany()
                   .HasForeignKey(x => x.MenuItemId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

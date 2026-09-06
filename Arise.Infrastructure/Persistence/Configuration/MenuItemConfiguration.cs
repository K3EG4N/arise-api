using Arise.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arise.Infrastructure.Persistence.Configuration
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("menuItems", schema: "mnu");

            builder.HasKey(x => x.MenuItemId);

            builder.HasOne(x => x.Parent)
                   .WithMany()
                   .HasForeignKey(x => x.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Menu)
                   .WithMany()
                   .HasForeignKey(x => x.MenuId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

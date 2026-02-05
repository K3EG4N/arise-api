using arise_api.helpers;
using arise_api.Entities;
using arise_api.generic;
using Microsoft.EntityFrameworkCore;
using arise_api.entities;

namespace arise_api.provider
{
    public class AriseDbContext(DbContextOptions<AriseDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users", schema: "usr");
            modelBuilder.Entity<Employee>().ToTable("employees", schema: "emp");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTimeHelper.GetDateTimeNow();
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTimeHelper.GetDateTimeNow();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

using arise_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace arise_api.provider
{
    public class AriseDbContext(DbContextOptions<AriseDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users", schema: "usr");
        }
    }
}

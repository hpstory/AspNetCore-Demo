using Microsoft.EntityFrameworkCore;

namespace ManyToManyDemo
{
    internal class DemoDbContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                "Server=;Initial Catalog=BookStore;User ID=sa;Password=Qwer1234;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}

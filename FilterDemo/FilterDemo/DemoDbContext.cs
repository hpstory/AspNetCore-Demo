using Microsoft.EntityFrameworkCore;

namespace FilterDemo
{
    public class DemoDbContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WeatherForecast>(w =>
            {
                w.Property(s => s.Summary).HasMaxLength(10);
            });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityDemo
{
    public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<DemoDbContext>
    {
        public DemoDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<DemoDbContext> builder = new DbContextOptionsBuilder<DemoDbContext>();
            builder.UseSqlServer("Server=;Initial Catalog=Identity;User ID=sa;Password=Qwer1234;Connection Timeout=30;");
            return new DemoDbContext(builder.Options);
        }
    }
}

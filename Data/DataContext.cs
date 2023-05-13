using Microsoft.EntityFrameworkCore;

namespace ASP121.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Entity.User> Users121 { get; set; }

        
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}

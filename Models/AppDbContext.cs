using System.Data.Entity;

namespace Autoservice3.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("AutoserviceDbConnection")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

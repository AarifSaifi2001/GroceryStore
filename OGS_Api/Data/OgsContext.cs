using Microsoft.EntityFrameworkCore;

namespace OGS_Api.Data
{
    public class OgsContext : DbContext
    {
        
        public OgsContext(DbContextOptions<OgsContext> options): base(options)
        {
            
        }

        public DbSet<Categories> Categories { get; set; }
        
        public DbSet<Product> Products { get; set;}

        public DbSet<User> Users { get; set;}

        public DbSet<Orders> Orders { get; set;}
    }
}
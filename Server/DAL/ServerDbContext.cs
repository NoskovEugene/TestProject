using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Server.DAL
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext()
        {
        }

        public ServerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCart> ProductCarts { get; set; }
    }
}
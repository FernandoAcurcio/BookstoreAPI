using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalesItem> SalesItems { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

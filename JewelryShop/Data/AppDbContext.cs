using Microsoft.EntityFrameworkCore;
using JewelryShop.Models;

namespace JewelryShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Rating)
                .HasColumnType("numeric(2,1)"); 
        }
    }
}

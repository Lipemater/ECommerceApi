using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);

                entity.Property(c => c.Description).HasMaxLength(255);
            });

            modelBuilder.Entity<Product>(entity =>
            {
               entity.HasKey(p => p.Id);

               entity.Property(p => p.Name).IsRequired().HasMaxLength(150);
               
                entity.Property(p => p.Description).HasMaxLength(500);

                entity.Property(p => p.Price).IsRequired().HasColumnType("numeric(18,2)");

                entity.Property(p => p.StockQuantity).IsRequired();

                entity.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
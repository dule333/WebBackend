using Microsoft.EntityFrameworkCore;
using WebBackend.Models;

namespace WebBackend.Infrastructure
{
    public class DeliveryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DeliveryContext(DbContextOptions options) :base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliveryContext).Assembly);

            modelBuilder.Entity<Order>()
            .HasMany(x => x.Products)
            .WithMany(x => x.Orders)
            .UsingEntity<OrderProduct>(
                op => op
                    .HasOne(x => x.Product)
                    .WithMany(x => x.OrderProducts)
                    .HasForeignKey(x => x.ProductId),
                op => op.HasOne(x => x.Order)
                    .WithMany(x => x.OrderProducts)
                    .HasForeignKey(x => x.OrderId),
                op =>
                {
                    op.HasKey(x => new { x.OrderId, x.ProductId });
                });
        }
    }
}

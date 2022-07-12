using Microsoft.EntityFrameworkCore;
using WebBackend.Models;
using WebBackend.Dto;

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

        }

        public DbSet<WebBackend.Dto.ProductDto> ProductDto { get; set; }

        public DbSet<WebBackend.Dto.OrderDto> OrderDto { get; set; }
    }
}

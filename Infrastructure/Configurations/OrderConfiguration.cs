using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBackend.Models;

namespace WebBackend.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Customer)
                .WithOne(x => x.DeliveryCustomer)
                .HasForeignKey<Order>(x=>x.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            
        }
    }
}

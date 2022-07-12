using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBackend.Models;

namespace WebBackend.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Delivery)
                .WithOne(x => x.Postal)
                .HasForeignKey<Order>(x=>x.PostalId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

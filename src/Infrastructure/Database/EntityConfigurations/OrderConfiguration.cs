using Database.Extensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.DefaultConfiguration("Orders");

            builder
                .HasOne(x => x.User)
                .WithMany(y => y.Orders)
                .HasForeignKey(x => x.UserId);
        }
    }
}

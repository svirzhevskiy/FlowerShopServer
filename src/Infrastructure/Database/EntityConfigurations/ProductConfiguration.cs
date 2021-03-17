using Database.Extensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.DefaultConfiguration("Products");

            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300);
            builder.Property(x => x.Properties).HasColumnType("jsonb");

            builder
                .HasOne(x => x.Category)
                .WithMany(y => y.Products)
                .HasForeignKey(x => x.CategoryId);

            //builder
            //    .HasMany(x => x.Orders)
            //    .WithMany(y => y.Products);
        }
    }
}

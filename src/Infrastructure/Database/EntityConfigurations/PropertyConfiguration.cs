using Database.Extensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.DefaultConfiguration("Properties");

            builder.Property(x => x.Title).IsRequired().HasMaxLength(30);

            builder
                .HasMany(x => x.Categories)
                .WithMany(x => x.Properties);
        }
    }
}

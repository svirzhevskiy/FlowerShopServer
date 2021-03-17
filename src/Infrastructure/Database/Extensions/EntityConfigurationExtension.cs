using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Extensions
{
    public static class EntityConfigurationExtension
    {
        public static EntityTypeBuilder<T> DefaultConfiguration<T>(this EntityTypeBuilder<T> modelBuilder, string tableName)
            where T : class, IEntity
        {
            modelBuilder.ToTable(tableName);

            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            modelBuilder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

            return modelBuilder;
        }
    }
}

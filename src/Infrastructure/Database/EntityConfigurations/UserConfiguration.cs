using Database.Extensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.DefaultConfiguration("Users");

            builder.Property(x => x.Email).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Adress).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Password).IsRequired();

            builder
                .HasOne(x => x.Role)
                .WithMany(y => y.Users)
                .HasForeignKey(x => x.RoleId);
        }
    }
}

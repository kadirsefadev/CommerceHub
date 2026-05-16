

using CommerceHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommerceHub.Persistence.Configurations;

public class UserConfiguration:IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(x=>x.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired()
            .HasMaxLength(255);
        builder.Property(x => x.PasswordHash).IsRequired();
        builder.Property(x => x.Role).IsRequired()
            .HasMaxLength(50);


        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasQueryFilter(x=>!x.IsDeleted);


    }

}

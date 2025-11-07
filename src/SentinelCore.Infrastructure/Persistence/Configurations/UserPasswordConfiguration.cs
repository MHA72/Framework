using Microsoft.EntityFrameworkCore;
using SentinelCore.Core.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SentinelCore.Infrastructure.Persistence.Configurations;

public class UserPasswordConfiguration : IEntityTypeConfiguration<UserPassword>
{
    public void Configure(EntityTypeBuilder<UserPassword> builder)
    {
        builder.ToTable("UserPasswords", "auth");
        builder.HasKey(password => password.Id);
        builder.Property(password => password.HashedPassword).HasMaxLength(256).IsRequired();

        builder.HasOne(password => password.User)
            .WithMany(user => user.Passwords)
            .HasForeignKey(password => password.UserId);
    }
}
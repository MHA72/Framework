using Microsoft.EntityFrameworkCore;
using SentinelCore.Core.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SentinelCore.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "auth");
        builder.HasKey(u => u.Id);
        builder.Property(user => user.Email).HasMaxLength(100);
        builder.Property(user => user.FullName).HasMaxLength(100);
        builder.Property(user => user.Username).HasMaxLength(50).IsRequired();
        builder.Property(user => user.MobileNumber).HasMaxLength(15).IsRequired();
    }
}
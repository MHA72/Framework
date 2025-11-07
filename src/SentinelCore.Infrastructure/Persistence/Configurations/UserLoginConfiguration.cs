using Microsoft.EntityFrameworkCore;
using SentinelCore.Core.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SentinelCore.Infrastructure.Persistence.Configurations;

public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable("UserLogins", "auth");
        builder.HasKey(userLogin => userLogin.Id);

        builder.Property(userLogin => userLogin.IpAddress).HasMaxLength(45);
        builder.Property(userLogin => userLogin.UserAgent).HasMaxLength(512);

        builder.HasOne(userLogin => userLogin.User)
            .WithMany(user => user.Logins)
            .HasForeignKey(userLogin => userLogin.UserId);
    }
}
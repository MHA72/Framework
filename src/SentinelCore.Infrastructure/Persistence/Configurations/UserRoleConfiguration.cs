using Microsoft.EntityFrameworkCore;
using SentinelCore.Core.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SentinelCore.Infrastructure.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles", "auth");
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.HasOne(userRole => userRole.User)
            .WithMany(user => user.Roles)
            .HasForeignKey(userRole => userRole.UserId);

        builder.HasOne(userRole => userRole.Role)
            .WithMany(role => role.Users)
            .HasForeignKey(userRole => userRole.RoleId);
    }
}
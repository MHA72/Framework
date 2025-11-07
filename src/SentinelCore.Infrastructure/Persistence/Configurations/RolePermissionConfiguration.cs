using Microsoft.EntityFrameworkCore;
using SentinelCore.Core.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SentinelCore.Infrastructure.Persistence.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions", "auth");
        builder.HasKey(rolePermission => rolePermission.Id);

        builder.HasOne(rolePermission => rolePermission.Role)
            .WithMany(role => role.Permissions)
            .HasForeignKey(rolePermission => rolePermission.RoleId);

        builder.HasOne(rolePermission => rolePermission.Permission)
            .WithMany(permission => permission.Roles)
            .HasForeignKey(rolePermission => rolePermission.PermissionId);
    }
}
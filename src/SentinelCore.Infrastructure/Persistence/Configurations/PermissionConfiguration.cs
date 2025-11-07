using Microsoft.EntityFrameworkCore;
using SentinelCore.Core.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SentinelCore.Infrastructure.Persistence.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions", "auth");
        builder.HasKey(permission => permission.Id);

        builder.Property(permission => permission.Key).HasMaxLength(100).IsRequired();
        builder.Property(permission => permission.Description).HasMaxLength(200);
    }
}
using Microsoft.EntityFrameworkCore;
using SentinelCore.Core.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SentinelCore.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "auth");
        builder.HasKey(role => role.Id);
        builder.Property(role => role.Name).HasMaxLength(100).IsRequired();
        builder.Property(role => role.Description).HasMaxLength(200);
    }
}
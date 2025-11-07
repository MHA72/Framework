using Microsoft.EntityFrameworkCore;
using SentinelCore.Core.Entities.AuditLog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SentinelCore.Infrastructure.Persistence.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs", "log");
        builder.HasKey(auditLog => auditLog.Id);

        builder.Property(auditLog => auditLog.ActionType).HasMaxLength(50).IsRequired();
        builder.Property(auditLog => auditLog.EntityName).HasMaxLength(100).IsRequired();
        builder.Property(auditLog => auditLog.EntityId).HasMaxLength(50);
        builder.Property(auditLog => auditLog.Message).HasMaxLength(1000).IsRequired();
        builder.Property(auditLog => auditLog.StackTrace).HasMaxLength(4000);
        builder.Property(auditLog => auditLog.IpAddress).HasMaxLength(45);
        builder.Property(auditLog => auditLog.UserAgent).HasMaxLength(512);
    }
}
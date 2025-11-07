using SentinelCore.Core.Models;

namespace SentinelCore.Application.Interfaces.AuditLog;

public interface IAuditService
{
    Task LogAsync(AuditLogRequest auditLogRequest);
}

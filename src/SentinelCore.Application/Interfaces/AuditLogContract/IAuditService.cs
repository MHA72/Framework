using SentinelCore.Core.Models.Request;

namespace SentinelCore.Application.Interfaces.AuditLogContract;

public interface IAuditService
{
    Task LogAsync(AuditLogRequest auditLogRequest);
}

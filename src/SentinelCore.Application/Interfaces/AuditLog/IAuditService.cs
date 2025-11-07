using SentinelCore.Core.Models;

namespace SentinelCore.Application.Interfaces;

public interface IAuditService
{
    Task LogAsync(AuditLogRequest auditLogRequest);
}

using SentinelCore.Core.Models.Request;

namespace SentinelCore.Application.Contracts.AuditLogContract;

public interface IAuditContract
{
    Task LogAsync(AuditLogRequest auditLogRequest);
}

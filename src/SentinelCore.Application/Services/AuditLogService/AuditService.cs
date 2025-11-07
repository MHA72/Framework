using Microsoft.AspNetCore.Http;
using SentinelCore.Core.Models.Request;
using SentinelCore.Infrastructure.Persistence;
using SentinelCore.Application.Interfaces.AuditLogContract;

namespace SentinelCore.Application.Services.AuditLogService;

public class AuditService(AppDbContext context, IHttpContextAccessor http) : IAuditService
{
    public async Task LogAsync(AuditLogRequest auditLogRequest)
    {
        var log = new Core.Entities.AuditLog.AuditLog
        {
            ActionType = auditLogRequest.ActionType,
            EntityName = auditLogRequest.EntityName,
            EntityId = auditLogRequest.EntityId,
            Message = auditLogRequest.Message,
            StackTrace = auditLogRequest.Exception?.ToString(),
            IpAddress = http.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
            UserAgent = http.HttpContext?.Request?.Headers["User-Agent"],
            ActorUserId = GetCurrentUserId(),
            CreateTime = DateTime.UtcNow
        };

        context.AuditLogs.Add(log);
        await context.SaveChangesAsync();
    }

    private Guid? GetCurrentUserId()
    {
        var userIdClaim = http.HttpContext?.User.FindFirst("userId");
        return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : null;
    }
}

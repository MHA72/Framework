using SentinelCore.Core.Models;
using Microsoft.AspNetCore.Http;
using SentinelCore.Application.Interfaces;
using SentinelCore.Core.Entities.AuditLog;
using SentinelCore.Infrastructure.Persistence;

namespace SentinelCore.Application.Services;

public class AuditService(AppDbContext context, IHttpContextAccessor http) : IAuditService
{
    public async Task LogAsync(AuditLogRequest auditLogRequest)
    {
        var log = new AuditLog
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

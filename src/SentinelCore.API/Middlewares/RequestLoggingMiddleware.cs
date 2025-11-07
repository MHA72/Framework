using SentinelCore.Core.Models;
using SentinelCore.Application.Interfaces;

namespace SentinelCore.API.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IAuditService audit)
    {
        var request = context.Request;

        var log = new AuditLogRequest
        {
            ActionType = "Request",
            EntityName = "HttpRequest",
            EntityId = null,
            Message = $"[{request.Method}] {request.Path} from {context.Connection.RemoteIpAddress}",
        };

        await audit.LogAsync(log);

        await next(context);
    }
}
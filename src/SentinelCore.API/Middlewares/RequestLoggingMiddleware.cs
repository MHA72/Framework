using SentinelCore.Core.Models.Request;
using SentinelCore.Application.Interfaces.AuditLogContract;

namespace SentinelCore.API.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IAuditService audit)
    {
        var request = context.Request;

        var log = new AuditLogRequest(null, null,
            $"[{request.Method}] {request.Path} from {context.Connection.RemoteIpAddress}", "Request", "HttpRequest");

        await audit.LogAsync(log);

        await next(context);
    }
}
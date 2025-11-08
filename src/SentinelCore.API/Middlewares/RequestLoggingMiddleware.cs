using SentinelCore.Application.Contracts.AuditLogContract;
using SentinelCore.Core.Models.Request;

namespace SentinelCore.API.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IAuditContract audit)
    {
        var request = context.Request;

        var log = new AuditLogRequest(null, null,
            $"[{request.Method}] {request.Path} from {context.Connection.RemoteIpAddress}", "Request", "HttpRequest");

        await audit.LogAsync(log);

        await next(context);
    }
}
using SentinelCore.Application.Interfaces;
using SentinelCore.Core.Models;

namespace SentinelCore.API.Middlewares;

public class ExceptionLoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IAuditService audit)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var request = context.Request;

            var log = new AuditLogRequest
            {
                ActionType = "Exception",
                EntityName = "HttpRequest",
                EntityId = null,
                Message = $"Unhandled exception in [{request.Method}] {request.Path}",
                Exception = ex
            };

            await audit.LogAsync(log);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("خطای غیرمنتظره‌ای رخ داده است.");
        }
    }
}
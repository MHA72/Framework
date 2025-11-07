using SentinelCore.Core.Models.Request;
using SentinelCore.Application.Interfaces.AuditLogContract;

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

            var log = new AuditLogRequest(null, ex, $"Unhandled exception in [{request.Method}] {request.Path}",
                "Exception", "HttpRequest");

            await audit.LogAsync(log);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("خطای غیرمنتظره‌ای رخ داده است.");
        }
    }
}
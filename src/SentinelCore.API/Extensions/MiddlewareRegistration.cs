using SentinelCore.API.Middlewares;

namespace SentinelCore.API.Extensions;

public static class MiddlewareRegistration
{
    public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionLoggingMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();

        return app;
    }
}

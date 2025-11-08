using System.Security.Claims;
using SentinelCore.Application.Contracts.PermissionContract;

namespace SentinelCore.API.Middlewares;

public class PermissionMiddleware
{
    private readonly RequestDelegate _next;
    public PermissionMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context, IPermissionContract permissionService)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var requiredPermission = context.GetEndpoint()?.Metadata.GetMetadata<PermissionAttribute>()?.Key;

        if (requiredPermission != null && !await permissionService.HasPermissionAsync(Guid.Parse(userId!), requiredPermission))
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access Denied");
            return;
        }

        await _next(context);
    }
}

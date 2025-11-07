using SentinelCore.Application.Services;
using SentinelCore.Application.Interfaces;

namespace SentinelCore.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IAuditService, AuditService>();
        
        return services;
    }
}
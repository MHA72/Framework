using SentinelCore.Application.Services;
using SentinelCore.Application.Interfaces;
using SentinelCore.Application.Interfaces.AuditLog;
using SentinelCore.Application.Services.AuditLog;

namespace SentinelCore.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IAuditService, AuditService>();
        
        services.AddJwtAuthentication(configuration);

        
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocumentation();
        
        return services;
    }
}
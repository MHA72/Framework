using SentinelCore.Application.Services.AuditLogService;
using SentinelCore.Application.Services.SecurityService;
using SentinelCore.Application.Interfaces.AuditLogContract;
using SentinelCore.Application.Interfaces.SecurityContract;

namespace SentinelCore.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IAuditService, AuditService>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        
        services.AddJwtAuthentication(configuration);

        
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocumentation();
        
        return services;
    }
}
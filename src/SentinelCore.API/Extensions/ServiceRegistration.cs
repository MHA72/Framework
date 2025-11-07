namespace SentinelCore.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthContract, AuthService>();
        services.AddScoped<IUserContract, UserService>();
        services.AddScoped<IRoleContract, RoleService>();
        services.AddScoped<IAuditService, AuditService>();
        services.AddScoped<IPermissionContract, PermissionService>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddJwtAuthentication(configuration);

        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocumentation(configuration);
        services.AddAuthorization();

        return services;
    }
}
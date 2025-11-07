using System.Text;
using Microsoft.IdentityModel.Tokens;

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
        services.AddScoped<ITokenContract, TokenService>();
        services.AddScoped<IPermissionContract, PermissionService>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
                    )
                };
            });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSwaggerDocumentation(configuration);
        services.AddAuthorization();

        return services;
    }
}
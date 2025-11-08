using System.Reflection;
using SentinelCore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using SentinelCore.Core.Entities.User;
using SentinelCore.Core.Entities.AuditLog;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SentinelCore.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // DbSets
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<UserLogin> UserLogins => Set<UserLogin>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<UserPassword> UserPasswords => Set<UserPassword>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<SmsVerificationCode> SmsVerificationCodes => Set<SmsVerificationCode>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(AppDbContext)
                    .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)!
                    .MakeGenericMethod(entityType.ClrType);

                method.Invoke(null, [modelBuilder]);
            }
        }
    }

    private static void SetSoftDeleteFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : BaseEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDelete);
    }
}
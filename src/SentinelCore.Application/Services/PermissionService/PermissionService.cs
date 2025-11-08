using Microsoft.EntityFrameworkCore;
using SentinelCore.Application.Contracts.PermissionContract;
using SentinelCore.Core.Entities.User;
using SentinelCore.Infrastructure.Persistence;

namespace SentinelCore.Application.Services.PermissionService;

public class PermissionService(AppDbContext context) : IPermissionContract
{
    public async Task<bool> HasPermissionAsync(Guid userId, string permissionKey)
    {
        return await context.UserRoles
            .Where(ur => ur.UserId == userId)
            .SelectMany(ur => ur.Role!.Permissions!)
            .AnyAsync(rp => rp.Permission!.Key == permissionKey);
    }

    public async Task<Permission> CreatePermission(string key, string? description)
    {
        var permission = new Permission { Id = Guid.NewGuid(), Key = key, Description = description };
        context.Add(permission);
        await context.SaveChangesAsync();
        return permission;
    }

    public async Task UpdatePermission(Guid permissionId, string key, string? description)
    {
        await DeletePermission(permissionId);
        await CreatePermission(key, description);
    }

    public async Task DeletePermission(Guid permissionId)
    {
        var permission =
            await context.RolePermissions.FirstAsync(rolePermission => rolePermission.PermissionId == permissionId);
        permission.DeleteTime = DateTime.Now;
        //TODO Add DeleteUserId
        context.Update(permission);
        await context.SaveChangesAsync();
    }

    public Task<List<Permission>> GetAllPermission() => context.Permissions.ToListAsync();

    public async Task AssignPermissionToRole(Guid roleId, Guid permissionId)
    {
        var rp = new RolePermission { Id = Guid.NewGuid(), RoleId = roleId, PermissionId = permissionId };
        context.Add(rp);
        await context.SaveChangesAsync();
    }

    public async Task RemovePermissionFromRole(Guid roleId, Guid permissionId)
    {
        var rp = await context.RolePermissions.FirstAsync(r => r.RoleId == roleId && r.PermissionId == permissionId);
        context.Remove(rp);
        await context.SaveChangesAsync();
    }
}
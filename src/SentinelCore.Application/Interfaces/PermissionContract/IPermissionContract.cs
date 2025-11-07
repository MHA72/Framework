using SentinelCore.Core.Entities.User;

namespace SentinelCore.Application.Interfaces.PermissionContract;

public interface IPermissionContract
{
    Task DeletePermission(Guid permissionId);
    Task<List<Permission>> GetAllPermission();
    Task AssignPermissionToRole(Guid roleId, Guid permissionId);
    Task RemovePermissionFromRole(Guid roleId, Guid permissionId);
    Task<Permission> CreatePermission(string key, string? description);
    Task UpdatePermission(Guid permissionId, string key, string? description);
}
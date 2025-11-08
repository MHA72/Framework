using SentinelCore.Core.Entities.User;

namespace SentinelCore.Application.Contracts.RoleContract;

public interface IRoleContract
{
    Task DeleteAsync(Guid roleId);
    Task<List<Role>> GetAllAsync();
    Task AddRoleToUserAsync(Guid userId, Guid roleId);
    Task RemoveRoleFromUserAsync(Guid userId, Guid roleId);
    Task<Role> CreateAsync(string name, string? description);
    Task UpdateAsync(Guid roleId, string name, string? description);
}
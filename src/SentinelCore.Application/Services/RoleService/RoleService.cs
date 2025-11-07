using Microsoft.EntityFrameworkCore;
using SentinelCore.Application.Interfaces.RoleContract;
using SentinelCore.Core.Entities.User;
using SentinelCore.Infrastructure.Persistence;

namespace SentinelCore.Application.Services.RoleService;

public class RoleContract(AppDbContext context) : IRoleContract
{
    public async Task<Role> CreateAsync(string name, string? description)
    {
        var role = new Role { Id = Guid.NewGuid(), Name = name, Description = description };
        context.Add(role);
        await context.SaveChangesAsync();
        return role;
    }

    public async Task UpdateAsync(Guid roleId, string name, string? description)
    {
        await DeleteAsync(roleId);
        await CreateAsync(name, description);
    }

    public async Task DeleteAsync(Guid roleId)
    {
        var role = await context.Roles.FirstAsync(role => role.Id == roleId);
        role.DeleteTime = DateTime.Now;
        role.IsDelete = true;
        context.Update(role);
        await context.SaveChangesAsync();
    }

    public Task<List<Role>> GetAllAsync() => context.Set<Role>().ToListAsync();

    public async Task AddRoleToUserAsync(Guid userId, Guid roleId)
    {
        var userRole = new UserRole { Id = Guid.NewGuid(), UserId = userId, RoleId = roleId };
        context.Add(userRole);
        await context.SaveChangesAsync();
    }

    public async Task RemoveRoleFromUserAsync(Guid userId, Guid roleId)
    {
        var userRole = await context.Set<UserRole>().FirstAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        context.Remove(userRole);
        await context.SaveChangesAsync();
    }
}

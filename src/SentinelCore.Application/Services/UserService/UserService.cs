using Microsoft.EntityFrameworkCore;
using SentinelCore.Application.Contracts.SecurityContract;
using SentinelCore.Application.Contracts.UserContract;
using SentinelCore.Core.Entities.User;
using SentinelCore.Core.Models.Request;
using SentinelCore.Infrastructure.Persistence;

namespace SentinelCore.Application.Services.UserService;

public class UserService(AppDbContext context, IPasswordHasher passwordHasher) : IUserContract
{
    public Task<User> GetByIdAsync(Guid id) => 
        context.Users
            .Include(user => user.Roles)!
        .ThenInclude(role => role.Role)
        .FirstAsync(user => user.Id == id);

    public async Task RegisterAsync(RegisterRequest request)
    {
        var existing = await context.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username || u.MobileNumber == request.MobileNumber);

        if (existing != null)
            throw new InvalidOperationException("کاربری با این نام یا شماره همراه وجود دارد.");

        var user = new User
        {
            Username = request.Username,
            Password = passwordHasher.Hash(request.Password),
            MobileNumber = request.MobileNumber
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
    
    public Task<List<User>> GetAllAsync() => context.Users.ToListAsync();

    public Task<User> GetByUsernameAsync(string username) =>
        context.Users
            .Include(user => user.Roles)!
            .ThenInclude(role => role.Role)
            .FirstAsync(user => user.Username == username);

    public async Task ChangePasswordAsync(Guid userId, string newPassword)
    {
        var user = await GetByIdAsync(userId);
        await DeleteUserById(userId);
        var request = new RegisterRequest(user.Username, user.MobileNumber, passwordHasher.Hash(newPassword));
        await RegisterAsync(request);
        await context.SaveChangesAsync();
    }
    public async Task DeleteUserById(Guid userId)
    {
        var user = await GetByIdAsync(userId);
        user.IsActive = false;
        user.IsDelete = true;
        user.DeleteTime = DateTime.Now;
        //Todo user.DeleteUserId = userId;
        await context.SaveChangesAsync();
    }
    
    public Task<List<UserRole>> GetUserRolesAsync(Guid userId) =>
        context.Set<UserRole>().Include(ur => ur.Role).Where(ur => ur.UserId == userId).ToListAsync();

    public async Task<List<Permission>> GetUserPermissionsAsync(Guid userId)
    {
        var roles = await GetUserRolesAsync(userId);
        var roleIds = roles.Select(r => r.RoleId).ToList();

        return await context.RolePermissions
            .Include(rp => rp.Permission)
            .Where(rp => roleIds.Contains(rp.RoleId))
            .Select(rp => rp.Permission!)
            .Distinct()
            .ToListAsync();
    }
}
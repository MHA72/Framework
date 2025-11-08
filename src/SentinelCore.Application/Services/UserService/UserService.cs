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
            .Include(user => user.Passwords)
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
            IsActive = true,
            Email = request.Email,
            FullName = request.FullName,
            Username = request.Username,
            Roles = new List<UserRole>(),
            MobileNumber = request.MobileNumber,
            Passwords = new List<UserPassword>(),
            Password = passwordHasher.Hash(request.Password),
        };

        var userPassword = new UserPassword
        {
            UserId = user.Id,
            IsActive = user.IsActive,
            HashedPassword = user.Password,
        };
        user.Passwords.Add(userPassword);

        foreach (var roleId in request.RoleIds)
        {
            var userRole = new UserRole
            {
                RoleId = roleId,
                UserId = user.Id
            };
            user.Roles.Add(userRole);
        }

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
        var roleIds = user.Roles!.Select(role => role.RoleId).ToList();
        await DeleteUserById(userId);
        var request = new RegisterRequest(user.FullName, user.Email, user.Username, user.MobileNumber,
            passwordHasher.Hash(newPassword), roleIds);
        await RegisterAsync(request);
        await context.SaveChangesAsync();
    }

    public async Task DeleteUserById(Guid userId)
    {
        var user = await GetByIdAsync(userId);
        foreach (var userRole in user.Roles!)
        {
            userRole.IsDelete = true;
            userRole.DeleteUserId = userId;
            userRole.DeleteTime = DateTime.Now;
        }
        foreach (var userPassword in user.Passwords!)
        {
            userPassword.IsActive = false;
            userPassword.IsDelete = true;
            userPassword.DeleteUserId = userId;
            userPassword.DeleteTime = DateTime.Now;
        }
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
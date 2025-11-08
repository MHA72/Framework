using SentinelCore.Core.Entities.User;
using SentinelCore.Core.Models.Request;

namespace SentinelCore.Application.Contracts.UserContract;

public interface IUserContract
{
    Task<List<User>> GetAllAsync();
    Task DeleteUserById(Guid userId);
    Task<User> GetByIdAsync(Guid id);
    Task RegisterAsync(RegisterRequest request);
    Task<User> GetByUsernameAsync(string username);
    Task<List<UserRole>> GetUserRolesAsync(Guid userId);
    Task ChangePasswordAsync(Guid userId, string newPassword);
    Task<List<Permission>> GetUserPermissionsAsync(Guid userId);
}
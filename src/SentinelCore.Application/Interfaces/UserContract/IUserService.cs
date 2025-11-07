using SentinelCore.Core.Models;
using SentinelCore.Core.Entities.User;
using SentinelCore.Core.Models.Request;

namespace SentinelCore.Application.Interfaces.UserContract;


public interface IUserService
{
    Task<User> GetByIdAsync(Guid id);
    Task RegisterAsync(RegisterRequest request);
    Task<User> GetByUsernameAsync(string username);
}

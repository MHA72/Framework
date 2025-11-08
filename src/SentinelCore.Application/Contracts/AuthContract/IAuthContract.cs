using SentinelCore.Core.Entities.User;
using SentinelCore.Core.Models.Request;
using SentinelCore.Core.Models.Response;

namespace SentinelCore.Application.Contracts.AuthContract;

public interface IAuthContract
{
    Task<string> GenerateTokenAsync(User user);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}
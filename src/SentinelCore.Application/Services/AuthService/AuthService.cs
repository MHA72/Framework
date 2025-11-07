using SentinelCore.Core.Entities.User;
using SentinelCore.Core.Models.Request;
using SentinelCore.Core.Models.Response;
using SentinelCore.Application.Interfaces.UserContract;
using SentinelCore.Application.Interfaces.AuthContract;
using SentinelCore.Application.Interfaces.SecurityContract;

namespace SentinelCore.Application.Services.AuthService;

public class AuthService(IPasswordHasher hasher, IUserContract userContract, ITokenContract tokenContract)
    : IAuthContract
{
    public Task<string> GenerateTokenAsync(User user)
    {
        var token = tokenContract.CreateToken(user);
        return Task.FromResult(token);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await userContract.GetByUsernameAsync(request.Username);

        if (!hasher.Verify(request.Password, user.Password))
            throw new UnauthorizedAccessException("نام کاربری یا رمز عبور اشتباه است.");

        var token = tokenContract.CreateToken(user);

        return new AuthResponse(token, user.Username, user.Roles!.First().ToString()!);
    }
}
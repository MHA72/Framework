using SentinelCore.Core.Entities.User;
using SentinelCore.Core.Models.Request;
using SentinelCore.Core.Models.Response;
using SentinelCore.Application.Interfaces.UserContract;
using SentinelCore.Application.Interfaces.AuthContract;
using SentinelCore.Application.Interfaces.SecurityContract;

namespace SentinelCore.Application.Services.AuthService;

public class AuthService(IPasswordHasher hasher, IUserService userService, ITokenService tokenService) : IAuthService
{
    public Task<string> GenerateTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await userService.GetByUsernameAsync(request.Username);

        if (!hasher.Verify(request.Password, user.Password))
            throw new UnauthorizedAccessException("نام کاربری یا رمز عبور اشتباه است.");

        var token = tokenService.CreateToken(user);

        return new AuthResponse(token, user.Username, user.Roles!.First().ToString()!);
    }
}

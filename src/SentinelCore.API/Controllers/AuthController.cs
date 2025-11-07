using Microsoft.AspNetCore.Mvc;
using SentinelCore.Application.Interfaces.AuthContract;
using SentinelCore.Application.Interfaces.UserContract;
using SentinelCore.Core.Models.Request;

namespace SentinelCore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, IUserService userService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await authService.LoginAsync(request);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        await userService.RegisterAsync(request);
        return Ok(new { message = "ثبت‌نام با موفقیت انجام شد." });
    }
}

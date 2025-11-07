using Microsoft.AspNetCore.Mvc;
using SentinelCore.Core.Models.Request;

namespace SentinelCore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthContract authContract, IUserContract userContract) : ControllerBase
{

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await authContract.LoginAsync(request);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        await userContract.RegisterAsync(request);
        return Ok(new { message = "ثبت‌نام با موفقیت انجام شد." });
    }
}


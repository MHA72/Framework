using Microsoft.AspNetCore.Mvc;
using SentinelCore.Application.Contracts.UserContract;

namespace SentinelCore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserContract userContract) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await userContract.GetByIdAsync(id);
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await userContract.GetAllAsync();
        return Ok(users);
    }

    [HttpPut("{id}/change-password")]
    public async Task<IActionResult> ChangePassword(Guid id, [FromBody] string newPassword)
    {
        await userContract.ChangePasswordAsync(id, newPassword);
        return Ok(new { message = "رمز عبور با موفقیت تغییر کرد." });
    }

    [HttpGet("{id}/roles")]
    public async Task<IActionResult> GetRoles(Guid id)
    {
        var roles = await userContract.GetUserRolesAsync(id);
        return Ok(roles);
    }

    [HttpGet("{id}/permissions")]
    public async Task<IActionResult> GetPermissions(Guid id)
    {
        var permissions = await userContract.GetUserPermissionsAsync(id);
        return Ok(permissions);
    }
}

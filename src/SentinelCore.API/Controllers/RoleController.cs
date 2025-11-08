using Microsoft.AspNetCore.Mvc;
using SentinelCore.Application.Contracts.RoleContract;
using SentinelCore.Core.Models.Request;

namespace SentinelCore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleContract _roleContract;

    public RoleController(IRoleContract roleContract)
    {
        _roleContract = roleContract;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoleCreateRequest request)
    {
        var role = await _roleContract.CreateAsync(request.Name, request.Description);
        return Ok(role);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] RoleCreateRequest request)
    {
        await _roleContract.UpdateAsync(id, request.Name, request.Description);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _roleContract.DeleteAsync(id);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _roleContract.GetAllAsync();
        return Ok(roles);
    }

    [HttpPost("{roleId}/assign-to-user/{userId}")]
    public async Task<IActionResult> AssignToUser(Guid roleId, Guid userId)
    {
        await _roleContract.AddRoleToUserAsync(userId, roleId);
        return Ok();
    }

    [HttpDelete("{roleId}/remove-from-user/{userId}")]
    public async Task<IActionResult> RemoveFromUser(Guid roleId, Guid userId)
    {
        await _roleContract.RemoveRoleFromUserAsync(userId, roleId);
        return Ok();
    }
}

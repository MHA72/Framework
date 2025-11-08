using Microsoft.AspNetCore.Mvc;
using SentinelCore.Application.Contracts.PermissionContract;
using SentinelCore.Core.Models.Request;

namespace SentinelCore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionController(IPermissionContract permissionContract) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PermissionCreateRequest request)
    {
        var permission = await permissionContract.CreatePermission(request.Key, request.Description);
        return Ok(permission);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PermissionCreateRequest request)
    {
        await permissionContract.UpdatePermission(id, request.Key, request.Description);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await permissionContract.DeletePermission(id);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var permissions = await permissionContract.GetAllPermission();
        return Ok(permissions);
    }

    [HttpPost("{permissionId}/assign-to-role/{roleId}")]
    public async Task<IActionResult> AssignToRole(Guid permissionId, Guid roleId)
    {
        await permissionContract.AssignPermissionToRole(roleId, permissionId);
        return Ok();
    }

    [HttpDelete("{permissionId}/remove-from-role/{roleId}")]
    public async Task<IActionResult> RemoveFromRole(Guid permissionId, Guid roleId)
    {
        await permissionContract.RemovePermissionFromRole(roleId, permissionId);
        return Ok();
    }
}

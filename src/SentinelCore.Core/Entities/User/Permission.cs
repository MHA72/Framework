namespace SentinelCore.Core.Entities.User;

public class Permission : BaseEntity
{
    public required string Key { get; set; }
    public string? Description { get; set; }
    public ICollection<RolePermission>? Roles { get; set; }
}

namespace SentinelCore.Core.Entities.User;

public class Permission : BaseEntity
{
    public required string Key { get; init; }
    public string? Description { get; init; }
    public ICollection<RolePermission>? Roles { get; set; }
}

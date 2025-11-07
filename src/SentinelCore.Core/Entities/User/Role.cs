namespace SentinelCore.Core.Entities.User;

public class Role : BaseEntity
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public ICollection<UserRole>? Users { get; init; }
    public ICollection<RolePermission>? Permissions { get; init; }
}

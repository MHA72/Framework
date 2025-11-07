namespace SentinelCore.Core.Entities.User;

public class Role : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<UserRole>? Users { get; set; }
    public ICollection<RolePermission>? Permissions { get; set; }
}

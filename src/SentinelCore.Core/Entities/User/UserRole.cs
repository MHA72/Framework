namespace SentinelCore.Core.Entities.User;

public class UserRole : BaseEntity
{
    public Guid UserId { get; init; }
    public User? User { get; init; }
    public Guid RoleId { get; init; }
    public Role? Role { get; init; }
}

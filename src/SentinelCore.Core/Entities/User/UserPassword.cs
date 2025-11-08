namespace SentinelCore.Core.Entities.User;

public class UserPassword : BaseEntity
{
    public Guid UserId { get; init; }
    public User? User { get; init; }
    public bool IsActive { get; set; } = true;
    public required string HashedPassword { get; init; }
    public DateTime ExpireAt { get; init; } = DateTime.Now.AddMonths(1);
}

namespace SentinelCore.Core.Entities.User;

public class UserPassword : BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public bool IsActive { get; set; } = true;
    public required string HashedPassword { get; set; }
    public DateTime ExpireAt { get; set; } = DateTime.UtcNow.AddMonths(1);
}

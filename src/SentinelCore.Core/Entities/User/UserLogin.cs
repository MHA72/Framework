namespace SentinelCore.Core.Entities.User;

public class UserLogin : BaseEntity
{
    public Guid UserId { get; init; }
    public User? User { get; init; }
    public string? IpAddress { get; init; }
    public string? UserAgent { get; init; }
    public bool IsSuccessful { get; init; }
}

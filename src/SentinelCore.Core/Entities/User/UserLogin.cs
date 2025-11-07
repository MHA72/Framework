namespace SentinelCore.Core.Entities.User;

public class UserLogin : BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public bool IsSuccessful { get; set; }
}

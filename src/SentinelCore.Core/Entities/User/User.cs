namespace SentinelCore.Core.Entities.User;

public class User : BaseEntity
{
    public string? Email { get; init; }
    public string? FullName { get; init; }
    public bool IsActive { get; set; } = true;
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string MobileNumber { get; init; }
    public ICollection<UserRole>? Roles { get; init; }
    public ICollection<UserLogin>? Logins { get; init; }
    public ICollection<UserPassword>? Passwords { get; init; }
}

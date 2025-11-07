namespace SentinelCore.Core.Entities.User;

public class User : BaseEntity
{
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public bool IsActive { get; set; } = true;
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string MobileNumber { get; set; }
    public ICollection<UserRole>? Roles { get; set; }
    public ICollection<UserLogin>? Logins { get; set; }
    public ICollection<UserPassword>? Passwords { get; set; }
}

namespace SentinelCore.Core.Models.Request;

public sealed record RegisterRequest(
    string? FullName,
    string? Email,
    string Username,
    string MobileNumber,
    string Password,
    List<Guid> RoleIds);
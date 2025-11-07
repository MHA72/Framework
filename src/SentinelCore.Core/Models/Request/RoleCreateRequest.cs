namespace SentinelCore.Core.Models.Request;

public sealed record RoleCreateRequest(
    string Name,
    string? Description
);
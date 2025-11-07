namespace SentinelCore.Core.Models.Request;

public sealed record PermissionCreateRequest(
    string Key,
    string? Description
);
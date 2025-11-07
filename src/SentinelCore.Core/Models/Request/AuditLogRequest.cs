namespace SentinelCore.Core.Models.Request;

public sealed record AuditLogRequest(
    string? EntityId,
    Exception? Exception,
    string Message,
    string ActionType,
    string EntityName);
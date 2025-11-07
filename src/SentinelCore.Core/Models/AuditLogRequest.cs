namespace SentinelCore.Core.Models;

public class AuditLogRequest
{
    public string? EntityId { get; set; }
    public Exception? Exception { get; set; }
    public required string Message { get; set; }
    public required string ActionType { get; set; }
    public required string EntityName { get; set; }
}

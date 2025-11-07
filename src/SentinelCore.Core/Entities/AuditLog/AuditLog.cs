namespace SentinelCore.Core.Entities.AuditLog;

public class AuditLog : BaseEntity
{
    public required string ActionType { get; set; }
    public required string EntityName { get; set; }
    public string? EntityId { get; set; }
    public required string Message { get; set; }
    public string? StackTrace { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public Guid? ActorUserId { get; set; }
}
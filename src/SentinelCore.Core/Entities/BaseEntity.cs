namespace SentinelCore.Core.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    public Guid? CreateUserId { get; set; }
    public bool IsDelete { get; set; } = false;
    public DateTime? DeleteTime { get; set; }
    public Guid? DeleteUserId { get; set; }
}
namespace SentinelCore.Core.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreateTime { get; init; } = DateTime.Now;
    public Guid? CreateUserId { get; set; }
    public bool IsDelete { get; set; }
    public DateTime? DeleteTime { get; set; }
    public Guid? DeleteUserId { get; set; }
}
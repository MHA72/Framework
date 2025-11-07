namespace SentinelCore.Core.Entities.User;

public class SmsVerificationCode : BaseEntity
{
    public required string MobileNumber { get; set; }
    public required string Code { get; init; }
    public DateTime SentAt { get; init; }
    public DateTime ExpireAt { get; init; } = DateTime.UtcNow.AddMinutes(2);
    public bool IsUsed { get; init; }
}
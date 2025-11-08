namespace SentinelCore.Core.Entities.User;

public class SmsVerificationCode : BaseEntity
{
    public required string MobileNumber { get; init; }
    public required string Code { get; init; }
    public DateTime SentAt { get; init; }
    public DateTime ExpireAt { get; init; } = DateTime.Now.AddMinutes(2);
    public bool IsUsed { get; set; }
}
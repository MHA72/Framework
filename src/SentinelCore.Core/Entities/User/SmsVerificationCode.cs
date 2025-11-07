namespace SentinelCore.Core.Entities.User;

public class SmsVerificationCode : BaseEntity
{
    public required string MobileNumber { get; set; }
    public required string Code { get; set; }
    public DateTime SentAt { get; set; }
    public DateTime ExpireAt { get; set; } = DateTime.UtcNow.AddMinutes(2);
    public bool IsUsed { get; set; } = false;
}
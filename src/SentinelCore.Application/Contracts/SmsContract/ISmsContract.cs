namespace SentinelCore.Application.Contracts.SmsContract;

public interface ISmsContract
{
    Task SendCodeAsync(string mobileNumber);
    Task<bool> VerifyCodeAsync(string mobile, string code);
}
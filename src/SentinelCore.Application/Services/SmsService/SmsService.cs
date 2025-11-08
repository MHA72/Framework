using Microsoft.EntityFrameworkCore;
using SentinelCore.Application.Contracts.SmsContract;
using SentinelCore.Core.Entities.User;
using SentinelCore.Infrastructure.Persistence;

namespace SentinelCore.Application.Services.SmsService;

public class SmsService(AppDbContext context) : ISmsContract
{
    public async Task SendCodeAsync(string mobile)
    {
        var code = new Random().Next(100000, 999999).ToString();
        //Todo Send SMS
        await context.SmsVerificationCodes.AddAsync(new SmsVerificationCode
        {
            MobileNumber = mobile,
            Code = code,
            SentAt = DateTime.Now,
            ExpireAt = DateTime.Now.AddMinutes(2),
            IsUsed = false
        });
        await context.SaveChangesAsync();
    }
    public async Task<bool> VerifyCodeAsync(string mobile, string code)
    {
        var record = await context.SmsVerificationCodes
            .FirstOrDefaultAsync(c => c.MobileNumber == mobile && c.Code == code && !c.IsUsed && c.ExpireAt > DateTime.Now);

        if (record == null) return false;
        record.IsUsed = true;
        await context.SaveChangesAsync();
        return true;
    }

}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SentinelCore.Core.Entities.User;

namespace SentinelCore.Infrastructure.Persistence.Configurations;

public class SmsVerificationCodeConfiguration : IEntityTypeConfiguration<SmsVerificationCode>
{
    public void Configure(EntityTypeBuilder<SmsVerificationCode> builder)
    {
        builder.ToTable("SmsVerificationCodes", "auth");
        builder.HasKey(smsVerificationCode => smsVerificationCode.Id);

        builder.Property(smsVerificationCode => smsVerificationCode.MobileNumber).HasMaxLength(15).IsRequired();
        builder.Property(smsVerificationCode => smsVerificationCode.Code).HasMaxLength(10).IsRequired();
    }
}
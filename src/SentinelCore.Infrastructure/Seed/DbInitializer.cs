using System.Text;
using System.Security.Cryptography;
using SentinelCore.Core.Entities.User;
using SentinelCore.Infrastructure.Persistence;

namespace SentinelCore.Infrastructure.Seed;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (context.Users.Any(u => u.Username == "admin")) return;
        var adminRole = new Role { Name = "Admin", Description = "مدیر سیستم" };
        var adminUser = new User
        {
            Username = "admin",
            FullName = "ادمین سیستم",
            IsActive = true,
            MobileNumber = "09100000000",
            Password = Hash("12345"),
        };

        var password = new UserPassword
        {
            User = adminUser,
            HashedPassword = Hash("12345"),
            IsActive = true,
            ExpireAt = DateTime.UtcNow.AddMonths(1)
        };

        context.Roles.Add(adminRole);
        context.Users.Add(adminUser);
        context.UserPasswords.Add(password);
        context.UserRoles.Add(new UserRole { User = adminUser, Role = adminRole });

        context.SaveChanges();
    }

    private static string Hash(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
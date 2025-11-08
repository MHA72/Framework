using System.Text;
using System.Security.Cryptography;
using SentinelCore.Application.Contracts.SecurityContract;

namespace SentinelCore.Application.Services.SecurityService;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }

    public bool Verify(string password, string hashed)
    {
        return Hash(password) == hashed;
    }
}

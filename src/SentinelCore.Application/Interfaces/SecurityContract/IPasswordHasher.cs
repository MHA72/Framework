namespace SentinelCore.Application.Interfaces.SecurityContract;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hashed);
}
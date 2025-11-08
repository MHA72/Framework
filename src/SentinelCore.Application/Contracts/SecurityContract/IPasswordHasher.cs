namespace SentinelCore.Application.Contracts.SecurityContract;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hashed);
}
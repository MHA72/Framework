using SentinelCore.Core.Entities.User;

namespace SentinelCore.Application.Interfaces.AuthContract;

public interface ITokenService
{
    string CreateToken(User user);
}

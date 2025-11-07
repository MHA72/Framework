using SentinelCore.Core.Entities.User;

namespace SentinelCore.Application.Interfaces.AuthContract;

public interface ITokenContract
{
    string CreateToken(User user);
}

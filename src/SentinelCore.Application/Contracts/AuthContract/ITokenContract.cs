using SentinelCore.Core.Entities.User;

namespace SentinelCore.Application.Contracts.AuthContract;

public interface ITokenContract
{
    string CreateToken(User user);
}

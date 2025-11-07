using Microsoft.EntityFrameworkCore;
using SentinelCore.Core.Entities.User;
using SentinelCore.Core.Models.Request;
using SentinelCore.Infrastructure.Persistence;
using SentinelCore.Application.Interfaces.UserContract;

namespace SentinelCore.Application.Services.UserService;

public class UserService(AppDbContext context) : IUserService
{
    public Task<User> GetByIdAsync(Guid id) => context.Users.FirstAsync(user => user.Id == id);

    public async Task RegisterAsync(RegisterRequest request)
    {
        var existing = await context.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username || u.MobileNumber == request.MobileNumber);

        if (existing != null)
            throw new InvalidOperationException("کاربری با این نام یا شماره همراه وجود دارد.");

        var user = new User
        {
            Username = request.Username,
            Password = request.Password,
            MobileNumber = request.MobileNumber
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public Task<User> GetByUsernameAsync(string username) =>
        context.Users.FirstAsync(user => user.Username == username);
}
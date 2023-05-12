using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserAPI.Core;
using UserAPI.Entities.Models;
using UserAPI.Entities.Resources;

namespace UserAPI.Persistence.repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext context;

    public UserRepository(UserDbContext context)
    {
        this.context = context;
    }
    public async Task<string> Add(User user)
    {
        var existingUser = await GetUser(user.UserName);

        if (existingUser != null)
        {
            return $"User with username '{user.UserName}' already exists.";
        }

        await context.Users.AddAsync(user);
        return string.Empty;
    }

    public async Task<User> GetUser(string userName)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }
}
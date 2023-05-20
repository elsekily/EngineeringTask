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
    public async Task Add(User user)
    {
        await context.Users.AddAsync(user);
    }

    public async Task<User> GetUser(string userName)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }
}
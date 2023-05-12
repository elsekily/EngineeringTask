using UserAPI.Entities.Models;
using UserAPI.Entities.Resources;

namespace UserAPI.Core;

public interface IUserRepository
{
    Task<string> Add(User user);
    Task<User> GetUser(string userName);
}
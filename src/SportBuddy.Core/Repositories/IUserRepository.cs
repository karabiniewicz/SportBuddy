using SportBuddy.Core.Entities;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(UserId id);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByUsernameAsync(Username username);
    Task AddAsync(User user);
}
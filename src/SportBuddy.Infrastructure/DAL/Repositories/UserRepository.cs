using Microsoft.EntityFrameworkCore;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Infrastructure.DAL.Repositories;

internal sealed class UserRepository(SportBuddyDbContext dbContext) : IUserRepository
{
    private readonly DbSet<User> _users = dbContext.Users;

    public Task<User> GetByIdAsync(UserId id)
        => _users.SingleOrDefaultAsync(x => x.Id == id);

    public Task<User> GetByEmailAsync(Email email)
        => _users.SingleOrDefaultAsync(x => x.Email == email);

    public Task<User> GetByUsernameAsync(Username username)
        => _users.SingleOrDefaultAsync(x => x.Username == username);

    public async Task AddAsync(User user)
        => await _users.AddAsync(user);
}
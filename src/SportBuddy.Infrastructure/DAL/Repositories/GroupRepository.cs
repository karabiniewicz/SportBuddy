using Microsoft.EntityFrameworkCore;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;

namespace SportBuddy.Infrastructure.DAL.Repositories;

internal sealed class GroupRepository(SportBuddyDbContext dbContext) : IGroupRepository
{
    private readonly DbSet<Group> _groups = dbContext.Groups;

    public async Task<IEnumerable<Group>> GetAllAsync() 
        => await _groups.ToListAsync();
    
    public async Task<Group> GetAsync(Guid id)
        => await _groups.SingleOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(Group group) 
        => await _groups.AddAsync(group);

    public Task UpdateAsync(Group group)
    {
        _groups.Update(group);
        return Task.CompletedTask;
    }
}
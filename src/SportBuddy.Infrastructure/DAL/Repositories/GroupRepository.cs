using Microsoft.EntityFrameworkCore;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Infrastructure.DAL.Repositories;

internal sealed class GroupRepository(SportBuddyDbContext dbContext) : IGroupRepository
{
    private readonly DbSet<Group> _groups = dbContext.Groups;

    public async Task<IEnumerable<Group>> GetAllAsync() 
        => await _groups.ToListAsync();
    
    public async Task<Group> GetAsync(GroupId id)
        => await _groups
            .Include(x => x.Members)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<Group> GetByNameAsync(GroupName name)
        => await _groups.SingleOrDefaultAsync(x => x.Name == name);
    
    public async Task AddAsync(Group group) 
        => await _groups.AddAsync(group);

    public async Task UpdateAsync(Group group)
    {
        _groups.Update(group);
        await Task.CompletedTask;
    }
}
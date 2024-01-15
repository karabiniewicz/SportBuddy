using SportBuddy.Core.Entities;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Core.Repositories;

public interface IGroupRepository
{
    Task<IEnumerable<Group>> GetAllAsync();
    Task<Group> GetAsync(GroupId id);
    Task AddAsync(Group group);
    Task UpdateAsync(Group group);
}
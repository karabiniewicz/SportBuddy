using SportBuddy.Core.Entities;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Core.Repositories;

public interface IGroupRepository
{
    Task<IEnumerable<Group>> GetAllUserAsync(UserId userId);
    Task<Group> GetAsync(GroupId id);
    Task<Group> GetByNameAsync(GroupName name);
    Task AddAsync(Group group);
    Task UpdateAsync(Group group);
}
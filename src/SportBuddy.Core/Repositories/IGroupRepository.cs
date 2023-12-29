using SportBuddy.Core.Entities;

namespace SportBuddy.Core.Repositories;

public interface IGroupRepository
{
    Task<IEnumerable<Group>> GetAllAsync();
    Task<Group> GetAsync(Guid id);
    Task AddAsync(Group group);
    Task UpdateAsync(Group group);
}
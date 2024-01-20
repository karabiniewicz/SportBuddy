using SportBuddy.Core.Entities;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Core.Repositories;

public interface IMatchRepository
{
    Task<IEnumerable<Match>> GetAllAsync();
    Task<IEnumerable<Match>> GetArchivedMatchesAsync(GroupId groupId, DateOnly today);
    Task<IEnumerable<Match>> GetUpcomingMatchesAsync(GroupId groupId, DateOnly today);
    Task<IEnumerable<Match>> GetByUserIdAndDateAsync(UserId userId, DateOnly date);
    Task<Match> GetAsync(MatchId id);
    Task<Match> GetByNameAsync(MatchName name);
    Task AddAsync(Match match);
    Task UpdateAsync(Match match);
}
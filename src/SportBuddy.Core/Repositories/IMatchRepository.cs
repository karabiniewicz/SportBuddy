using SportBuddy.Core.Entities;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Core.Repositories;

public interface IMatchRepository
{
    Task<IEnumerable<Match>> GetAllAsync();
    Task<Match> GetAsync(MatchId id);
    Task<Match> GetByNameAsync(MatchName name);
    Task AddAsync(Match match);
    Task UpdateAsync(Match match);
}
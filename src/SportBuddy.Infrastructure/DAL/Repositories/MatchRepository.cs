using Microsoft.EntityFrameworkCore;
using SportBuddy.Core.Entities;
using SportBuddy.Core.Repositories;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Infrastructure.DAL.Repositories;

internal sealed class MatchRepository(SportBuddyDbContext dbContext) : IMatchRepository
{
    private readonly DbSet<Match> _matches = dbContext.Matches;
    public async Task AddAsync(Match match)
        => await _matches.AddAsync(match);

    public async Task UpdateAsync(Match match)
    {
        _matches.Update(match);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Match>> GetAllAsync()
        => await _matches.ToListAsync();

    public async Task<Match> GetAsync(MatchId id)
        => await _matches
            .Include(x => x.Members)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<Match> GetByNameAsync(MatchName name)
        => await _matches.SingleOrDefaultAsync(x => x.Name == name);

    public async Task<IEnumerable<Match>> GetArchivedMatchesAsync(GroupId groupId, DateOnly today)
        => await _matches
            .Where(x => x.GroupId == groupId && x.Date < today)
            .ToListAsync();

    public async Task<IEnumerable<Match>> GetUpcomingMatchesAsync(GroupId groupId, DateOnly today)
        => await _matches
            .Where(x => x.GroupId == groupId && x.Date >= today)
            .ToListAsync();

    public async Task<IEnumerable<Match>> GetByUserIdAndDateAsync(UserId userId, DateOnly date)
        => await _matches
            .Include(x => x.Members)
            .Where(x => x.Members.Any(m => m.Id == userId) && x.Date == date)
            .ToListAsync();
}
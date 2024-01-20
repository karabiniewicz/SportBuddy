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
}
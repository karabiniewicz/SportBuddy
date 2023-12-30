using SportBuddy.Application.DTO;
using SportBuddy.Core.Entities;

namespace SportBuddy.Application.Modules.Matches.Commands;

public static class Extensions
{
    public static MatchDto AsDto(this Match entity)
        => new(entity.Id, entity.Name, entity.Discipline, entity.Date);
}
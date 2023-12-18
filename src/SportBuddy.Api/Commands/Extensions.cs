using SportBuddy.Api.DTO;
using SportBuddy.Api.Entities;

namespace SportBuddy.Api.Commands;

public static class Extensions
{
    public static MatchDto AsDto(this Match entity)
        => new(entity.Id, entity.Name, entity.Discipline, entity.Date, entity.GroupName);
}
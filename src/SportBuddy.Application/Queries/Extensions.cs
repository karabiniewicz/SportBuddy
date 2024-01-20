using SportBuddy.Application.DTO;
using SportBuddy.Core.Entities;

namespace SportBuddy.Application.Queries;

public static class Extensions
{
    public static UserDto AsDto(this User entity)
        => new(entity.Id, entity.Username, entity.FullName);

    public static GroupDto AsDto(this Group entity)
        => new(entity.Id, entity.AdminId, entity.Name, entity.Description, entity.GroupType);

    public static GroupDto AsDtoWithMembers(this Group entity)
        => new(entity.Id, entity.AdminId, entity.Name, entity.Description, entity.GroupType, entity.Members.Select(x => x.AsDto()));

    public static MatchDto AsDto(this Match entity)
        => new(entity.Id, entity.Name, entity.Discipline, entity.Location, entity.Start, entity.End, entity.Date, entity.Charge, entity.Limit,
            entity.Members.Select(x => x.AsDto()));
}
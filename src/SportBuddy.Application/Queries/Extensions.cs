using SportBuddy.Application.DTO;
using SportBuddy.Core.Entities;

namespace SportBuddy.Application.Queries;

public static class Extensions
{
    public static UserDto AsDto(this User entity)
        => new(entity.Id, entity.Username, entity.FullName);
    
    public static GroupDto AsDto(this Group entity) 
        => new(entity.Id, entity.AdminId, entity.Name, entity.Description, entity.GroupType);
}
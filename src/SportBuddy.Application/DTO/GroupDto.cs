using SportBuddy.Core.Consts;

namespace SportBuddy.Application.DTO;

public record GroupDto(Guid Id, string Name, string Description, GroupType GroupType);
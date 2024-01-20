using SportBuddy.Core.Consts;

namespace SportBuddy.Application.DTO;

public record GroupDto(Guid Id, Guid AdminId, string Name, string Description, GroupType GroupType, IEnumerable<UserDto> Members = default);
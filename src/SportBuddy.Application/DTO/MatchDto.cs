using SportBuddy.Core.Consts;

namespace SportBuddy.Application.DTO;

public record MatchDto(
    Guid Id,
    string Name,
    Discipline Discipline,
    string Location,
    string Start,
    string End,
    DateOnly Date,
    decimal Charge,
    int Limit,
    IEnumerable<UserDto> Members = default!);
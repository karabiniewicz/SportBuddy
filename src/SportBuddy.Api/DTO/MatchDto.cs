using SportBuddy.Api.Consts;

namespace SportBuddy.Api.DTO;

public record MatchDto(Guid Id, string Name, Discipline Discipline, DateTimeOffset Date, string GroupName);
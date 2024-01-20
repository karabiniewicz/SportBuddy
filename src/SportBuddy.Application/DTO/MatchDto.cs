using SportBuddy.Core.Consts;

namespace SportBuddy.Application.DTO;

public record MatchDto(Guid Id, string Name, Discipline Discipline, DateTimeOffset Start, DateTimeOffset End, decimal Charge, int Limit);
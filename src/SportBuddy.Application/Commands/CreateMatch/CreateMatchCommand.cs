using SportBuddy.Application.Abstractions;
using SportBuddy.Core.Consts;
using SportBuddy.Core.ValueObjects;

namespace SportBuddy.Application.Commands.CreateMatch;

public record CreateMatchCommand(MatchId Id, string Name, Discipline Discipline, string Location, DateTimeOffset Start,
    DateTimeOffset End, decimal? Charge, int? Limit, GroupId GroupId, UserId UserId) : ICommand;
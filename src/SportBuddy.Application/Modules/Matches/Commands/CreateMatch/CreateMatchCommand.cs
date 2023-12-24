using SportBuddy.Core.Consts;

namespace SportBuddy.Application.Modules.Matches.Commands.CreateMatch;

public record CreateMatchCommand(string Name, Discipline Discipline, DateTimeOffset Date, string GroupName);
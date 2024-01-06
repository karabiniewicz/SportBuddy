using SportBuddy.Core.Consts;

namespace SportBuddy.Application.Commands.CreateMatch;

public record CreateMatchCommand(string Name, Discipline Discipline, DateTimeOffset Date);
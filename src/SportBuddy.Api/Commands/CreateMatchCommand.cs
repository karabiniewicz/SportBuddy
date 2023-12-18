using SportBuddy.Api.Consts;

namespace SportBuddy.Api.Commands;

public record CreateMatchCommand(string Name, Discipline Discipline, DateTimeOffset Date, string GroupName);
using SportBuddy.Application.Abstractions;

namespace SportBuddy.Application.Commands.SIgnIn;

public record SignInCommand(string Email, string Password) : ICommand;

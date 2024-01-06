using SportBuddy.Application.Abstractions;

namespace SportBuddy.Application.Commands.SignUp;

public sealed record SignUpCommand(Guid UserId, string Email, string Username, string Password, string FullName, string Role) : ICommand;
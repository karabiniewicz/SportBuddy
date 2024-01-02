using SportBuddy.Application.Abstractions;

namespace SportBuddy.Application.Modules.Users.Commands.SignUp;

public record SignUpCommand(Guid UserId, string Email, string Username, string Password, string FullName, string Role) : ICommand;
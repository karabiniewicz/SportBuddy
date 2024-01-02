namespace SportBuddy.Core.Entities;

public class User(Guid id, string email, string username, string password, string fullName, string role, DateTime createdAt)
{
    public Guid Id { get; private set; } = id;
    public string Email { get; private set; } = email;
    public string Username { get; private set; } = username;
    public string Password { get; private set; } = password;
    public string FullName { get; private set; } = fullName;
    public string Role { get; private set; } = role;
    public DateTime CreatedAt { get; private set; } = createdAt;
}
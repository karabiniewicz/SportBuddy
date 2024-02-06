namespace SportBuddy.Application.DTO;

public record RefreshTokenDto(string Token, DateTime ExpiryTime);
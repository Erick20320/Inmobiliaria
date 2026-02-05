namespace Inmobiliaria.Application.DTOs.Authentication;

public record AuthenticationResultDto
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
    public DateTime ExpiresAt { get; init; }
}

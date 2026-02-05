namespace Inmobiliaria.Application.DTOs.Authentication;

public sealed class UserAuthDto
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string PasswordHash { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}

namespace Inmobiliaria.Application.Abstractions.Security;

public interface IJwtService
{
    (string Token, DateTime ExpiresAt) GenerateToken(Guid userId, string email, string role);
}

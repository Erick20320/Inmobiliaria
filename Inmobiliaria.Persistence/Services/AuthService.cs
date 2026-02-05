using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Abstractions.Security;
using Inmobiliaria.Application.Abstractions.Services;
using Inmobiliaria.Application.DTOs.Authentication;
using Inmobiliaria.Application.Utilities.Responses;

namespace Inmobiliaria.Persistence.Services;

public sealed class AuthService : IAuthService
{
    private readonly IUserAuthRepository _repository;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserAuthRepository repository,
        IPasswordHasher hasher,
        IJwtService jwtService)
    {
        _repository = repository;
        _hasher = hasher;
        _jwtService = jwtService;
    }

    public async Task<ServiceResult<AuthenticationResultDto>> ValidateCredentialsAsync(
        string credential,
        string password,
        CancellationToken cancellationToken)
    {
        var user = await _repository.GetByCredentialAsync(credential);

        if (user is null)
        {
            return ServiceResult<AuthenticationResultDto>.Fail(
                "Credenciales inválidas",
                "AUTH_001"
            );
        }

        if (!user.IsActive)
        {
            return ServiceResult<AuthenticationResultDto>.Fail(
                "Usuario inactivo",
                "AUTH_002"
            );
        }

        if (!_hasher.Verify(password, user.PasswordHash))
        {
            return ServiceResult<AuthenticationResultDto>.Fail(
                "Credenciales inválidas",
                "AUTH_001"
            );
        }

        var roles = await _repository.GetRolesAsync(user.Id);
        var role = roles.FirstOrDefault() ?? "User";

        var token = _jwtService.GenerateToken(user.Id, user.Email, role);

        return ServiceResult<AuthenticationResultDto>.Success(
            new AuthenticationResultDto
            {
                UserId = user.Id,
                Email = user.Email,
                Role = role,
                Token = token.Token,
                ExpiresAt = token.ExpiresAt
            },
            message: "Autenticación correcta"
        );
    }
}

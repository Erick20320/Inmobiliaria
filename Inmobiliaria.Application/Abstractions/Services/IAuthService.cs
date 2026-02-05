using Inmobiliaria.Application.DTOs.Authentication;
using Inmobiliaria.Application.Utilities.Responses;

namespace Inmobiliaria.Application.Abstractions.Services;

public interface IAuthService
{
    Task<ServiceResult<AuthenticationResultDto>> ValidateCredentialsAsync(string credential, string password,
        CancellationToken cancellationToken);
}

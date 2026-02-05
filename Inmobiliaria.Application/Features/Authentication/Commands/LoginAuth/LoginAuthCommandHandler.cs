using Inmobiliaria.Application.Abstractions.Services;
using Inmobiliaria.Application.DTOs.Authentication;
using Inmobiliaria.Application.Utilities.Responses;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application.Features.Authentication.Commands.LoginAuth;

public sealed class LoginAuthCommandHandler : IRequestHandler<LoginAuthCommand, AuthenticationResultDto>
{
    private readonly IAuthService _authService;

    public LoginAuthCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<ServiceResult<AuthenticationResultDto>> Handle(LoginAuthCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _authService.ValidateCredentialsAsync(
            command.UserCredential,
            command.Password,
            cancellationToken);

        if (!result.Status)
        {
            return ServiceResult<AuthenticationResultDto>.Fail("Error en autenticación. Por favor, contacte al administrador.");
        }

        return result;
    }
}

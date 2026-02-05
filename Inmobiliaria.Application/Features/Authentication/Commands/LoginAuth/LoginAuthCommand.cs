using Inmobiliaria.Application.Abstractions.Mediator;
using Inmobiliaria.Application.DTOs.Authentication;

namespace Inmobiliaria.Application.Features.Authentication.Commands.LoginAuth;

public sealed record LoginAuthCommand(
    string UserCredential,
    string Password
) : IRequest<AuthenticationResultDto>;

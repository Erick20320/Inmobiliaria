using Inmobiliaria.API.Contracts.Responses;
using Inmobiliaria.API.Extensions;
using Inmobiliaria.Application.Abstractions.Mediator;
using Inmobiliaria.Application.DTOs.Authentication;
using Inmobiliaria.Application.Features.Authentication.Commands.LoginAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<AuthenticationResultDto>>> Login([FromBody] LoginUserRequest dto)
    {
        var command = dto.ToCommand();
        var result = await mediator.Send(command);

        return result.ToActionResult(this);
    }
}

using Inmobiliaria.API.Contracts.Responses;
using Inmobiliaria.API.Extensions;
using Inmobiliaria.Application.Abstractions.Mediator;
using Inmobiliaria.Application.Features.Properties.Commands.CreateProperty;
using Inmobiliaria.Application.Features.Properties.Commands.DeleteProperty;
using Inmobiliaria.Application.Features.Properties.Commands.UpdateProperty;
using Inmobiliaria.Application.Features.Properties.DTOs;
using Inmobiliaria.Application.Features.Properties.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.API.Controllers;

[ApiController]
[Route("api/properties")]
[Authorize]
public sealed class PropertiesController(IMediator mediator) : ControllerBase
{
    [HttpPost("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<Guid>>> Create([FromRoute] Guid id, [FromBody] CreatePropertyRequest request)
    {
        var result = await mediator.Send(request.ToCommand(id));
        return result.ToCreatedAtAction(
            this,
            nameof(GetById),
            new { id = result.Data }
        );
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<Guid>>> Update([FromRoute] Guid id, [FromBody] UpdatePropertyRequest request)
    {
        var result = await mediator.Send(request.ToCommand(id));
        return result.ToActionResult(this);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<Guid>>> Delete([FromRoute] Guid id)
    {
        var command = new DeletePropertyCommand { Id = id };
        var result = await mediator.Send(command);
        return result.ToActionResult(this);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<ApiResponse<List<GetPropertyAllQueryDto>>>> GetAll([FromQuery] GetPropertyAllQuery query)
    {
        var result = await mediator.Send(query);
        return result.ToActionResult(this);
    }

    [HttpGet("{id:Guid}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<ApiResponse<GetPropertyByIdQueryDto>>> GetById([FromRoute] Guid id)
    {
        var result = await mediator.Send(new GetPropertyByIdQuery { Id = id });
        return result.ToActionResult(this);
    }
}

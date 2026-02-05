using Inmobiliaria.API.Contracts.Responses;
using Inmobiliaria.API.Extensions;
using Inmobiliaria.Application.Abstractions.Mediator;
using Inmobiliaria.Application.Features.Agents.Commands.CreateAgent;
using Inmobiliaria.Application.Features.Agents.Commands.DeleteAgent;
using Inmobiliaria.Application.Features.Agents.Commands.UpdateAgent;
using Inmobiliaria.Application.Features.Agents.DTOs;
using Inmobiliaria.Application.Features.Agents.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.API.Controllers;

[ApiController]
[Route("api/agents")]
[Authorize]
public sealed class AgentsController(IMediator mediator) : ControllerBase
{
    [HttpPost("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<Guid>>> Create([FromRoute] Guid id, [FromBody] CreateAgentRequest request)
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
    public async Task<ActionResult<ApiResponse<Guid>>> Update([FromRoute] Guid id, [FromBody] UpdateAgentRequest request)
    {
        var result = await mediator.Send(request.ToCommand(id));
        return result.ToActionResult(this);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<Guid>>> Delete([FromRoute] Guid id)
    {
        var command = new DeleteAgentCommand { Id = id };
        var result = await mediator.Send(command);
        return result.ToActionResult(this);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<ApiResponse<List<GetAgentAllQueryDto>>>> GetAll([FromQuery] GetAgentAllQuery query)
    {
        var result = await mediator.Send(query);
        return result.ToActionResult(this);
    }

    [HttpGet("{id:Guid}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<ApiResponse<GetAgentByIdQueryDto>>> GetById([FromRoute] Guid id)
    {
        var result = await mediator.Send(new GetAgentByIdQuery { Id = id });
        return result.ToActionResult(this);
    }
}

using Inmobiliaria.Application.Abstractions.Mediator;
using Inmobiliaria.Application.Features.Agents.DTOs;

namespace Inmobiliaria.Application.Features.Agents.Queries;

public class GetAgentByIdQuery : IRequest<GetAgentByIdQueryDto>
{
    public Guid Id { get; set; }
}

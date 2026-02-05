using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Features.Agents.DTOs;
using Inmobiliaria.Application.Features.Agents.Mappers;
using Inmobiliaria.Application.Features.Agents.Queries;
using Inmobiliaria.Application.Utilities.Responses;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application.Features.Agents.Handlers.Queries;

public sealed class GetAgentByIdQueryHandler
    : IRequestHandler<GetAgentByIdQuery, GetAgentByIdQueryDto>
{
    private readonly IAgentRepository _repository;

    public GetAgentByIdQueryHandler(IAgentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult<GetAgentByIdQueryDto>> Handle(
        GetAgentByIdQuery request, CancellationToken cancellationToken)
    {
        var agent = await _repository.GetByIdAsync(request.Id);

        if (agent is null)
            return ServiceResult<GetAgentByIdQueryDto>.Warning(null, "Agente no encontrado.");

        var dto = agent.ToGetAgentByIdDto();

        return ServiceResult<GetAgentByIdQueryDto>.Success(dto, "Agente obtenido correctamente.");
    }
}

using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Features.Agents.DTOs;
using Inmobiliaria.Application.Features.Agents.Mappers;
using Inmobiliaria.Application.Features.Agents.Queries;
using Inmobiliaria.Application.Utilities.Responses;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application.Features.Agents.Handlers.Queries;

public sealed class GetAgentAllQueryHandler
    : IRequestHandler<GetAgentAllQuery, List<GetAgentAllQueryDto>>
{
    private readonly IAgentRepository _repository;

    public GetAgentAllQueryHandler(IAgentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult<List<GetAgentAllQueryDto>>> Handle(
        GetAgentAllQuery request, CancellationToken cancellationToken)
    {
        var agents = (await _repository.GetAllAsync()).ToList();

        var result = agents
            .Select(a => a.ToGetAgentAllDto())
            .ToList();

        return ServiceResult<List<GetAgentAllQueryDto>>.Success(result, "Agentes obtenidos correctamente.");
    }
}

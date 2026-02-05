using Inmobiliaria.Application.Abstractions.Mediator;
using Inmobiliaria.Application.Features.Agents.DTOs;

namespace Inmobiliaria.Application.Features.Agents.Queries;

public class GetAgentAllQuery : IRequest<List<GetAgentAllQueryDto>> { }

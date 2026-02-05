using Inmobiliaria.Application.Abstractions.Mediator;

namespace Inmobiliaria.Application.Features.Agents.Commands.DeleteAgent;

public class DeleteAgentCommand : IRequest<Guid>
{
    public required Guid Id { get; set; }
}

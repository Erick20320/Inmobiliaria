using Inmobiliaria.Application.Abstractions.Mediator;

namespace Inmobiliaria.Application.Features.Agents.Commands.CreateAgent;

public sealed class CreateAgentCommand : IRequest<Guid>
{
    public required Guid UserId { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public string? Phone { get; init; }
}

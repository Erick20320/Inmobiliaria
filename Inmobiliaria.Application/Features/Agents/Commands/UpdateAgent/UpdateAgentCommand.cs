using Inmobiliaria.Application.Abstractions.Mediator;

namespace Inmobiliaria.Application.Features.Agents.Commands.UpdateAgent;

public class UpdateAgentCommand : IRequest<Guid>
{
    public required Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; } = true;
}

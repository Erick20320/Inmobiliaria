using Inmobiliaria.Application.Abstractions.Mediator;
using Inmobiliaria.Domain.Enums;

namespace Inmobiliaria.Application.Features.Properties.Commands.CreateProperty;

public sealed record CreatePropertyCommand : IRequest<Guid>
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required PropertyType PropertyTypeId { get; init; }
    public required Guid AgentId { get; init; }
    public required decimal Price { get; init; }
    public required decimal AreaM2 { get; init; }
    public required string Address { get; init; }
    public required string City { get; init; }
}

using Inmobiliaria.Domain.Enums;

namespace Inmobiliaria.Application.DTOs.Properties;

public record PropertyDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required PropertyType PropertyTypeId { get; init; }
    public required Guid AgentId { get; init; }
    public required decimal Price { get; init; }
    public required decimal AreaM2 { get; init; }
    public required string Address { get; init; }
    public required string City { get; init; }
    public bool IsActive { get; init; }
}

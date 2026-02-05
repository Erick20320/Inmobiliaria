using Inmobiliaria.Domain.Enums;

namespace Inmobiliaria.Application.Features.Properties.Commands.CreateProperty;

public sealed class CreatePropertyRequest
{
    public string Title { get; init; } = null!;
    public string? Description { get; init; }

    public PropertyType PropertyTypeId { get; init; }

    public decimal Price { get; init; }
    public decimal AreaM2 { get; init; }

    public string Address { get; init; } = null!;
    public string City { get; init; } = null!;

    public bool? IsActive { get; init; } = true;

    public CreatePropertyCommand ToCommand(Guid agentId)
        => new()
        {
            AgentId = agentId,
            Title = Title.Trim(),
            Description = Description?.Trim(),
            PropertyTypeId = PropertyTypeId,
            Price = Price,
            AreaM2 = AreaM2,
            Address = Address.Trim(),
            City = City.Trim(),
        };
}

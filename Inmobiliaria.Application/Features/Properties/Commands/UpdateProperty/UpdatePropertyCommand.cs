using Inmobiliaria.Application.Abstractions.Mediator;

namespace Inmobiliaria.Application.Features.Properties.Commands.UpdateProperty;

public class UpdatePropertyCommand : IRequest<Guid>
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required Guid AgentId { get; set; }
    public required decimal Price { get; set; }
    public required decimal AreaM2 { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public bool IsActive { get; set; } = true;
}

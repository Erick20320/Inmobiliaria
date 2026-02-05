namespace Inmobiliaria.Application.Features.Properties.Commands.UpdateProperty;

public sealed class UpdatePropertyRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required Guid AgentId { get; set; }
    public required decimal Price { get; set; }
    public required decimal AreaM2 { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public bool IsActive { get; set; } = true;

    public UpdatePropertyCommand ToCommand(Guid id)
        => new()
        {
            Id = id,
            Title = Title.Trim(),
            Description = Description?.Trim(),
            AgentId = AgentId,
            Price = Price,
            AreaM2 = AreaM2,
            Address = Address.Trim(),
            City = City.Trim(),
            IsActive = IsActive
        };
}

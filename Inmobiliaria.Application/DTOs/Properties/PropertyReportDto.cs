using Inmobiliaria.Domain.Enums;

namespace Inmobiliaria.Application.DTOs.Properties;

public class PropertyReportDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public PropertyType PropertyTypeId { get; set; }
    public Guid AgentId { get; set; }
    public decimal Price { get; set; }
    public decimal AreaM2 { get; set; }
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

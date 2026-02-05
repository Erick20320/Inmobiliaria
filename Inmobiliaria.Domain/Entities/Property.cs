using Inmobiliaria.Domain.Enums;
using Inmobiliaria.Domain.Exceptions;

namespace Inmobiliaria.Domain.Entities;

public sealed class Property : BaseEntity
{
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public PropertyType PropertyTypeId { get; private set; }
    public Guid AgentId { get; private set; }
    public decimal Price { get; private set; }
    public decimal AreaM2 { get; private set; }
    public string Address { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public bool IsActive { get; private set; } = true;

    // Solo se usa para reportes con paginación
    public int TotalCount { get; private set; } = 0;

    // Constructor privado
    private Property() { }

    // Factory Method
    public static Property Create(
        string title,
        string? description,
        PropertyType propertyTypeId,
        Guid agentId,
        decimal price,
        decimal areaM2,
        string address,
        string city)
    {
        // Validaciones de dominio
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainRuleViolationException("El título es obligatorio.");

        if (string.IsNullOrWhiteSpace(address))
            throw new DomainRuleViolationException("La dirección es obligatoria.");

        if (price > 500_000)
            throw new DomainRuleViolationException("El precio no puede exceder $500,000.");

        if (areaM2 < 20 || areaM2 > 10_000)
            throw new DomainRuleViolationException("El área debe estar entre 20 y 10,000 m².");

        return new Property
        {
            Title = title,
            Description = description,
            PropertyTypeId = propertyTypeId,
            AgentId = agentId,
            Price = price,
            AreaM2 = areaM2,
            Address = address,
            City = city
        };
    }

    // Métodos para modificar la entidad

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainRuleViolationException("El título es obligatorio.");
        Title = title;
        MarkUpdated();
    }

    public void SetDescription(string? description)
    {
        Description = description;
        MarkUpdated();
    }

    public void SetPropertyTypeId(PropertyType type)
    {
        PropertyTypeId = type;
        MarkUpdated();
    }

    public void SetAgentId(Guid agentId)
    {
        AgentId = agentId;
        MarkUpdated();
    }

    public void UpdatePrice(decimal price)
    {
        if (price > 500_000)
            throw new DomainRuleViolationException("Precio inválido.");
        Price = price;
        MarkUpdated();
    }

    public void UpdateArea(decimal areaM2)
    {
        if (areaM2 < 20 || areaM2 > 10_000)
            throw new DomainRuleViolationException("Área inválida.");
        AreaM2 = areaM2;
        MarkUpdated();
    }

    public void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new DomainRuleViolationException("La dirección es obligatoria.");
        Address = address;
        MarkUpdated();
    }

    public void SetCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new DomainRuleViolationException("La ciudad es obligatoria.");
        City = city;
        MarkUpdated();
    }

    public void SetIsActive(bool isActive)
    {
        IsActive = isActive;
        MarkUpdated();
    }

    public void SetTotalCount(int totalCount)
    {
        TotalCount = totalCount;
    }

    // Métodos auxiliares para repositorios
    public void SetId(Guid id) => Id = id;
    public void SetCreatedAt(DateTime createdAt) => CreatedAt = createdAt;
    public void SetUpdatedAt(DateTime? updatedAt) => UpdatedAt = updatedAt;

    // Marca que la entidad
    private new void MarkUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}

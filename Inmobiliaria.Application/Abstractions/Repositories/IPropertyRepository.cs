using Inmobiliaria.Domain.Entities;
using Inmobiliaria.Domain.Enums;

namespace Inmobiliaria.Application.Abstractions.Repositories;

public interface IPropertyRepository
{
    Task<Guid> CreateAsync(Property property);
    Task<Property?> GetByIdAsync(Guid id);
    Task<IEnumerable<Property>> GetAllAsync();
    Task UpdateAsync(Property property);
    Task DeleteAsync(Guid id);

    // Reportes con paginación
    Task<(IReadOnlyCollection<Property> Properties, int TotalCount)> GetPropertiesByTypeAsync(
        PropertyType? propertyTypeId,
        int page = 1,
        int pageSize = 10,
        string sortBy = "Title",
        string sortDir = "ASC");

    Task<(IReadOnlyCollection<Property> Properties, int TotalCount)> GetPropertiesByHighestPriceAsync(
        int page = 1,
        int pageSize = 10,
        string sortBy = "Price",
        string sortDir = "DESC");
}

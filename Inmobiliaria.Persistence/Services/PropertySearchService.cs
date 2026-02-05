using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Abstractions.Services;
using Inmobiliaria.Application.DTOs.Properties;
using Inmobiliaria.Application.Features.Properties.DTOs;
using Inmobiliaria.Application.Utilities.Commons;

namespace Inmobiliaria.Persistence.Services;

public class PropertySearchService(IPropertyRepository repository) : IPropertySearchService
{
    private readonly IPropertyRepository _repository = repository;

    // Reporte por tipo de propiedad
    public async Task<PaginatedDto<PropertyReportDto>> SearchAsync(FilterPropertyReportDto filter,
        CancellationToken cancellationToken = default)
    {
        var (properties, totalCount) = await _repository.GetPropertiesByTypeAsync(
            propertyTypeId: filter.PropertyTypeId,
            page: filter.Page,
            pageSize: filter.PageSize,
            sortBy: filter.SortBy,
            sortDir: filter.SortDir
        );

        var data = properties.Select(MapToDto).ToList();

        return new PaginatedDto<PropertyReportDto>
        {
            Data = data,
            TotalCount = totalCount,
            Page = filter.Page,
            PageSize = filter.PageSize
        };
    }

    // Reporte de propiedades por mayor precio
    public async Task<PaginatedDto<PropertyReportDto>> SearchByHighestPriceAsync(FilterPropertyReportDto filter,
        CancellationToken cancellationToken = default)
    {
        var (properties, totalCount) = await _repository.GetPropertiesByHighestPriceAsync(
            page: filter.Page,
            pageSize: filter.PageSize,
            sortBy: filter.SortBy,
            sortDir: filter.SortDir
        );

        var data = properties.Select(MapToDto).ToList();

        return new PaginatedDto<PropertyReportDto>
        {
            Data = data,
            TotalCount = totalCount,
            Page = filter.Page,
            PageSize = filter.PageSize
        };
    }

    // Helper para mapear Property a DTO
    private static PropertyReportDto MapToDto(Domain.Entities.Property p) =>
        new()
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            PropertyTypeId = p.PropertyTypeId,
            AgentId = p.AgentId,
            Price = p.Price,
            AreaM2 = p.AreaM2,
            Address = p.Address,
            City = p.City,
            IsActive = p.IsActive,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            TotalCount = p.TotalCount
        };
}

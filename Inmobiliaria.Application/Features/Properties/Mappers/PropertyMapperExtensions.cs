using Inmobiliaria.Application.DTOs.Properties;
using Inmobiliaria.Application.Features.Properties.DTOs;
using Inmobiliaria.Domain.Entities;

namespace Inmobiliaria.Application.Features.Properties.Mappers;

public static class PropertyMapperExtensions
{
    public static GetPropertyAllQueryDto ToGetPropertyAllDto(this Property property)
        => new()
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            PropertyTypeId = property.PropertyTypeId,
            AgentId = property.AgentId,
            Price = property.Price,
            AreaM2 = property.AreaM2,
            Address = property.Address,
            City = property.City,
            IsActive = property.IsActive
        };

    public static GetPropertyByIdQueryDto ToGetPropertyByIdDto(this Property property)
        => new()
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            PropertyTypeId = property.PropertyTypeId,
            AgentId = property.AgentId,
            Price = property.Price,
            AreaM2 = property.AreaM2,
            Address = property.Address,
            City = property.City,
            IsActive = property.IsActive
        };

    //public static PropertyReportDto ToPropertyReportDto(this Property property, int totalCount)
    //    => new PropertyReportDto
    //    {
    //        Id = property.Id,
    //        Title = property.Title,
    //        Description = property.Description,
    //        PropertyTypeId = property.PropertyTypeId,
    //        AgentId = property.AgentId,
    //        Price = property.Price,
    //        AreaM2 = property.AreaM2,
    //        Address = property.Address,
    //        City = property.City,
    //        IsActive = property.IsActive,
    //        CreatedAt = property.CreatedAt,
    //        UpdatedAt = property.UpdatedAt,
    //        TotalCount = totalCount
    //    };
}

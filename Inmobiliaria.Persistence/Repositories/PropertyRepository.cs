using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Abstractions.Services;
using Inmobiliaria.Domain.Entities;
using Inmobiliaria.Domain.Enums;
using Inmobiliaria.Persistence.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Inmobiliaria.Persistence.Repositories;

public class PropertyRepository(IDbConnectionFactory factory) : BaseRepository(factory), IPropertyRepository
{
    #region CRUD

    public async Task<Guid> CreateAsync(Property property)
    {
        return await ExecuteScalarAsync<Guid>(
            "sp_CreateProperty",
            new SqlParameter("@Title", property.Title),
            new SqlParameter("@Description", property.Description ?? string.Empty),
            new SqlParameter("@PropertyTypeId", property.PropertyTypeId),
            new SqlParameter("@AgentId", property.AgentId),
            new SqlParameter("@Price", property.Price),
            new SqlParameter("@AreaM2", property.AreaM2),
            new SqlParameter("@Address", property.Address),
            new SqlParameter("@City", property.City),
            new SqlParameter("@IsActive", property.IsActive)
        );
    }

    public async Task<Property?> GetByIdAsync(Guid id)
    {
        return await ExecuteReaderSingleAsync(
            "sp_GetPropertyById",
            reader => MapProperty(reader),
            new SqlParameter("@Id", id)
        );
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await ExecuteReaderListAsync(
            "sp_GetAllProperties",
            reader => MapProperty(reader)
        );
    }

    public async Task UpdateAsync(Property property)
    {
        await ExecuteNonQueryAsync(
            "sp_UpdateProperty",
            new SqlParameter("@Id", property.Id),
            new SqlParameter("@Title", property.Title),
            new SqlParameter("@Description", property.Description ?? string.Empty),
            new SqlParameter("@PropertyTypeId", property.PropertyTypeId),
            new SqlParameter("@AgentId", property.AgentId),
            new SqlParameter("@Price", property.Price),
            new SqlParameter("@AreaM2", property.AreaM2),
            new SqlParameter("@Address", property.Address),
            new SqlParameter("@City", property.City),
            new SqlParameter("@IsActive", property.IsActive)
        );
    }

    public async Task DeleteAsync(Guid id)
    {
        await ExecuteNonQueryAsync(
            "sp_DeleteProperty",
            new SqlParameter("@Id", id)
        );
    }

    #endregion

    #region Reportes

    public async Task<(IReadOnlyCollection<Property> Properties, int TotalCount)> GetPropertiesByTypeAsync(
        PropertyType? propertyTypeId,
        int page = 1,
        int pageSize = 10,
        string sortBy = "Title",
        string sortDir = "ASC")
    {
        var properties = await ExecuteReaderListAsync(
            "sp_ReportPropertiesByType",
            reader => MapProperty(reader),
            new SqlParameter("@PropertyTypeId", (object?)propertyTypeId ?? DBNull.Value),
            new SqlParameter("@Page", page),
            new SqlParameter("@PageSize", pageSize),
            new SqlParameter("@SortBy", sortBy),
            new SqlParameter("@SortDir", sortDir)
        );

        int totalCount = properties.Count > 0 ? properties.First().TotalCount : 0;
        return (properties, totalCount);
    }

    public async Task<(IReadOnlyCollection<Property> Properties, int TotalCount)> GetPropertiesByHighestPriceAsync(
        int page = 1,
        int pageSize = 10,
        string sortBy = "Price",
        string sortDir = "DESC")
    {
        var properties = await ExecuteReaderListAsync(
            "sp_ReportPropertiesByHighestPrice",
            reader => MapProperty(reader, includeTotalCount: true),
            new SqlParameter("@Page", page),
            new SqlParameter("@PageSize", pageSize),
            new SqlParameter("@SortBy", sortBy),
            new SqlParameter("@SortDir", sortDir)
        );

        int totalCount = properties.Count > 0 ? properties.First().TotalCount : 0;
        return (properties, totalCount);
    }

    #endregion

    #region Helpers

    private static Property MapProperty(SqlDataReader reader, bool includeTotalCount = false)
    {
        var property = Property.Create(
            reader.GetStringOrEmpty("Title"),
            reader.GetStringOrEmpty("Description"),
            (PropertyType)reader.GetInt32("PropertyTypeId"),
            reader.GetGuidSafe("AgentId"),
            reader.GetDecimalSafe("Price"),
            reader.GetDecimalSafe("AreaM2"),
            reader.GetStringOrEmpty("Address"),
            reader.GetStringOrEmpty("City")
        );

        property.SetId(reader.GetGuidSafe("Id"));
        property.SetIsActive(reader.GetBooleanSafe("IsActive"));
        property.SetCreatedAt(reader.GetDateTimeSafe("CreatedAt"));
        property.SetUpdatedAt(reader.GetNullableValue<DateTime>("UpdatedAt"));

        property.SetTotalCount(reader.GetInt32Safe("TotalCount"));

        return property;
    }

    #endregion
}

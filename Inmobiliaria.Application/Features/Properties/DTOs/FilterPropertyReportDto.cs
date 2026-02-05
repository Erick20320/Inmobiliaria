using Inmobiliaria.Domain.Enums;

namespace Inmobiliaria.Application.Features.Properties.DTOs;

public class FilterPropertyReportDto
{
    public PropertyType? PropertyTypeId { get; set; }

    // Paginación
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    // Ordenamiento
    public string SortBy { get; set; } = "Title";
    public string SortDir { get; set; } = "ASC";
}

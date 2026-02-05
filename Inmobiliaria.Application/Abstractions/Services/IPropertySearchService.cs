using Inmobiliaria.Application.DTOs.Properties;
using Inmobiliaria.Application.Features.Properties.DTOs;
using Inmobiliaria.Application.Utilities.Commons;

namespace Inmobiliaria.Application.Abstractions.Services;

public interface IPropertySearchService
{
    Task<PaginatedDto<PropertyReportDto>> SearchAsync(
           FilterPropertyReportDto filter,
           CancellationToken cancellationToken = default);
}

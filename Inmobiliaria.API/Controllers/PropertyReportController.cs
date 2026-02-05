using Inmobiliaria.Application.Abstractions.Services;
using Inmobiliaria.Application.Features.Properties.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.API.Controllers;

[ApiController]
[Route("api/property-reports")]
[Authorize]
public class PropertyReportController : ControllerBase
{
    private readonly IPropertySearchService _searchService;

    public PropertyReportController(IPropertySearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("por-tipo")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetByType([FromQuery] FilterPropertyReportDto filter)
    {
        var result = await _searchService.SearchAsync(filter);

        HttpContext.Response.Headers["X-Total-Count"] = result.TotalCount.ToString();

        return Ok(result);
    }

    [HttpGet("propiedades-mayor-precio")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetPropertiesByHighestPrice([FromQuery] FilterPropertyReportDto filter)
    {
        var result = await _searchService.SearchAsync(filter);

        HttpContext.Response.Headers["X-Total-Count"] = result.TotalCount.ToString();

        return Ok(result);
    }
}

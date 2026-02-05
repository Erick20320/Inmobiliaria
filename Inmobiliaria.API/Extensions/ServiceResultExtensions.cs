using Inmobiliaria.API.Contracts.Responses;
using Inmobiliaria.Application.Utilities.Responses;
using Inmobiliaria.Domain.Enums.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.API.Extensions;

public static class ServiceResultExtensions
{
    public static ActionResult<ApiResponse<T>> ToActionResult<T>(this ServiceResult<T> result, ControllerBase controller)
    {
        var response = ApiResponse<T>.From(result);

        if (result.Status)
            return controller.Ok(response);

        var statusCode = MapToStatusCode(result.Error?.ErrorType);
        return controller.StatusCode(statusCode, response);
    }

    public static ActionResult<ApiResponse<T>> ToCreatedAtAction<T>(this ServiceResult<T> result, ControllerBase controller,
        string actionName, object routeValues)
    {
        var response = ApiResponse<T>.From(result);

        if (!result.Status)
        {
            var statusCode = MapToStatusCode(result.Error?.ErrorType);
            return controller.StatusCode(statusCode, response);
        }

        return controller.CreatedAtAction(actionName, routeValues, response);
    }

    private static int MapToStatusCode(ErrorType? errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Warning => StatusCodes.Status400BadRequest,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }
}

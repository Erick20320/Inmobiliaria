using Inmobiliaria.Application.Utilities.Responses;

namespace Inmobiliaria.API.Contracts.Responses;

public record ApiResponse<T>(
    T? Data,
    bool Status,
    string? Message,
    ServiceError? Error,
    DateTime Timestamp
)
{
    public static ApiResponse<T> From(ServiceResult<T> result)
        => new(
            result.Data,
            result.Status,
            result.Message,
            result.Error,
            result.Timestamp
        );
}

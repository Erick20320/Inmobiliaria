using Inmobiliaria.Domain.Enums.Errors;

namespace Inmobiliaria.Application.Utilities.Responses;

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; init; }

    public ServiceResult() { }

    public static ServiceResult<T> Success(T data, string? message = null, object? metadata = null) =>
        new()
        {
            Status = true,
            Data = data,
            Message = message,
            Metadata = metadata
        };

    public static ServiceResult<T> Warning(T? data, string? message = null, object? metadata = null) =>
        new()
        {
            Status = false,
            Data = data,
            Message = message,
            Metadata = metadata
        };

    public static ServiceResult<T> Fail(string message, string? code = null, IDictionary<string, string[]>? details = null) =>
        new()
        {
            Status = false,
            Data = default,
            Message = message,
            Error = new ServiceError
            {
                Code = code ?? "ERROR",
                Message = message,
                ErrorType = ErrorType.Failure,
                Details = details
            },
            Timestamp = DateTime.UtcNow
        };

    public static new ServiceResult<T> FailWithValidationErrors(IDictionary<string, string[]> errors)
    {
        return new ServiceResult<T>
        {
            Status = false,
            Data = default,
            Error = new ServiceError
            {
                ErrorType = ErrorType.Validation,
                Details = errors
            }
        };
    }
}

using Inmobiliaria.Domain.Enums.Errors;

namespace Inmobiliaria.Application.Utilities.Responses;

public class ServiceResult
{
    public bool Status { get; set; }
    public string? Message { get; set; }
    public ServiceError? Error { get; set; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;

    public object? Metadata { get; init; }

    public ServiceResult() { }

    public static ServiceResult Success(string? message = null, object? metadata = null) =>
    new()
    {
        Status = true,
        Message = message,
        Metadata = metadata
    };

    public static ServiceResult Fail(string? message = null, IDictionary<string, string[]>? details = null)
        => new()
        {
            Status = false,
            Message = message,
            Error = new ServiceError
            {
                Details = details
            }
        };

    public static ServiceResult Warning(string? message = null, IDictionary<string, string[]>? details = null)
        => new()
        {
            Status = false,
            Message = message,
            Error = new ServiceError
            {
                ErrorType = ErrorType.Warning,
                Details = details
            }
        };

    public static ServiceResult FailWithValidationErrors(IDictionary<string, string[]> errors)
    {
        return new ServiceResult
        {
            Status = false,
            Error = new ServiceError
            {
                ErrorType = ErrorType.Validation,
                Details = errors
            }
        };
    }
}

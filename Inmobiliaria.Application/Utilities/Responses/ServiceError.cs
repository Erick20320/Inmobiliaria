using Inmobiliaria.Domain.Enums.Errors;

namespace Inmobiliaria.Application.Utilities.Responses;

public class ServiceError
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public ErrorType ErrorType { get; set; }
    public IDictionary<string, string[]>? Details { get; set; }
    public string? StackTrace { get; set; }
}

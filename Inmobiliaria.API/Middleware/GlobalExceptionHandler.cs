using Inmobiliaria.Application.Exceptions;
using Inmobiliaria.Application.Utilities.Responses;
using Inmobiliaria.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Inmobiliaria.API.Middleware;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _environment;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Excepción no controlada: {ExceptionType} - {Message}",
            exception.GetType().Name, exception.Message);

        var (statusCode, result) = exception switch
        {
            ApplicationValidationException validationEx => HandleValidation(validationEx),
            MediatorException mediatorEx => HandleMediator(mediatorEx),
            NotFoundException notFoundEx => HandleNotFound(notFoundEx),
            InvalidOperationException invalidOpEx => HandleInvalidOperation(invalidOpEx),
            DomainRuleViolationException domainEx => HandleDomainRule(domainEx),
            _ => HandleUnexpected(exception)
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        if (_environment.IsDevelopment() && result.Error != null)
        {
            result.Error.StackTrace = exception.StackTrace;
        }

        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

        return true;
    }

    private static (int, ServiceResult<string>) HandleValidation(ApplicationValidationException ex)
    {
        var details = ex.ErrorsValidation
            .Select((e, i) => new { Key = $"Error{i + 1}", Value = new[] { e } })
            .ToDictionary(x => x.Key, x => x.Value);

        var result = ServiceResult<string>.FailWithValidationErrors(details);
        return ((int)HttpStatusCode.BadRequest, result);
    }

    private static (int, ServiceResult<string>) HandleMediator(MediatorException ex)
    {
        var result = ServiceResult<string>.Fail(ex.Message, "MEDIATOR_ERROR");
        return ((int)HttpStatusCode.BadRequest, result);
    }

    private static (int, ServiceResult<string>) HandleNotFound(NotFoundException ex)
    {
        ArgumentNullException.ThrowIfNull(ex);
        var result = ServiceResult<string>.Fail("El recurso solicitado no fue encontrado", "NOT_FOUND");
        return ((int)HttpStatusCode.NotFound, result);
    }

    private static (int, ServiceResult<string>) HandleInvalidOperation(InvalidOperationException ex)
    {
        var result = ServiceResult<string>.Fail(ex.Message, "INVALID_OPERATION");
        return ((int)HttpStatusCode.BadRequest, result);
    }

    private static (int, ServiceResult<string>) HandleDomainRule(DomainRuleViolationException ex)
    {
        var details = new Dictionary<string, string[]> { { "PropertyName", new[] { ex.PropertyName } } };
        var result = ServiceResult<string>.Fail(message: ex.Message, code: "DOMAIN_RULE_VIOLATION", details: details);
        return ((int)HttpStatusCode.BadRequest, result);
    }

    private static (int, ServiceResult<string>) HandleUnexpected(Exception ex)
    {
        var details = new Dictionary<string, string[]>
        {
            { "ExceptionType", new[] { ex.GetType().Name } },
            { "Message", new[] { ex.Message } }
        };

        var result = ServiceResult<string>.Fail(
            "Ocurrió un error interno en el servidor. Contacte al administrador.",
            "INTERNAL_SERVER_ERROR",
            details);

        return ((int)HttpStatusCode.InternalServerError, result);
    }
}

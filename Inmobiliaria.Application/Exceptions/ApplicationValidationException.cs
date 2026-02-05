
using FluentValidation.Results;

namespace Inmobiliaria.Application.Exceptions;

public sealed class ApplicationValidationException : Exception
{
    public List<string> ErrorsValidation { get; set; } = [];

    public ApplicationValidationException(string errorMessage)
    {
        ErrorsValidation.Add(errorMessage);
    }

    public ApplicationValidationException(ValidationResult validationResult)
    {
        foreach (var errorValidation in validationResult.Errors)
        {
            ErrorsValidation.Add(errorValidation.ErrorMessage);
        }
    }
}

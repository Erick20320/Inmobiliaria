using FluentValidation;
using Inmobiliaria.Application.Features.Agents.Commands.CreateAgent;

namespace Inmobiliaria.Application.Features.Agents.Validators;

public class CreateAgentValidator : AbstractValidator<CreateAgentCommand>
{
    public CreateAgentValidator()
    {
        ApplyCommonRules(this);
    }

    private static void ApplyCommonRules(AbstractValidator<CreateAgentCommand> validator)
    {
        validator.RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .MaximumLength(256);

        validator.RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .EmailAddress()
            .MaximumLength(256);

        validator.RuleFor(x => x.Phone)
            .Matches(@"^\+?\d{7,15}$").When(x => !string.IsNullOrEmpty(x.Phone));
    }
}

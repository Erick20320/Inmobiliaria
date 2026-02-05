using FluentValidation;
using Inmobiliaria.Application.Features.Agents.Commands.UpdateAgent;

namespace Inmobiliaria.Application.Features.Agents.Validators;

public class UpdateAgentValidator : AbstractValidator<UpdateAgentCommand>
{
    public UpdateAgentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} es requerido");

        ApplyCommonRules(this);
    }

    private static void ApplyCommonRules(AbstractValidator<UpdateAgentCommand> validator)
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

        validator.RuleFor(x => x.IsActive).NotNull();
    }
}

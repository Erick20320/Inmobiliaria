using FluentValidation;
using Inmobiliaria.Application.Features.Properties.Commands.CreateProperty;
using Inmobiliaria.Domain.Enums;

namespace Inmobiliaria.Application.Features.Properties.Validators;

public class CreatePropertyValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .MaximumLength(256).WithMessage("{PropertyName} no puede superar los 256 caracteres");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("{PropertyName} no puede superar los 1000 caracteres");

        RuleFor(x => x.PropertyTypeId)
            .IsInEnum().WithMessage("{PropertyName} no es un tipo de propiedad válido");

        RuleFor(x => x.Price)
            .InclusiveBetween(0, 500000).WithMessage("{PropertyName} debe ser menor o igual a $500,000");

        RuleFor(x => x.AreaM2)
            .InclusiveBetween(20, 10000).WithMessage("{PropertyName} debe estar entre 20 y 10,000 m²");

        RuleFor(x => x.AgentId)
            .NotEmpty().WithMessage("{PropertyName} es requerido")
            .MustAsync(async (agentId, ct) =>
            {
                return await AgentExistsAndActive(agentId);
            }).WithMessage("El agente no existe o no está activo");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("{PropertyName} es requerido")
            .MaximumLength(500).WithMessage("{PropertyName} no puede superar los 500 caracteres");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("{PropertyName} es requerido")
            .MaximumLength(100).WithMessage("{PropertyName} no puede superar los 100 caracteres");

        RuleFor(x => x.Title)
            .MustAsync(async (command, title, ct) =>
            {
                return await PropertyTitleTypeIsUnique(command.Title, command.PropertyTypeId);
            })
            .WithMessage("Ya existe una propiedad con este título y tipo");

        RuleFor(x => x.AgentId)
            .MustAsync(async (command, agentId, ct) =>
            {
                return await AgentHasLessThan20Properties(agentId);
            })
            .WithMessage("El agente no puede tener más de 20 propiedades asignadas");
    }

    private Task<bool> AgentExistsAndActive(Guid agentId)
    {
        return Task.FromResult(true);
    }

    private Task<bool> PropertyTitleTypeIsUnique(string title, PropertyType type)
    {
        return Task.FromResult(true);
    }

    private Task<bool> AgentHasLessThan20Properties(Guid agentId)
    {
        return Task.FromResult(true);
    }
}

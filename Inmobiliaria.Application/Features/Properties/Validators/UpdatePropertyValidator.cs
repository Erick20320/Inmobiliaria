using FluentValidation;
using Inmobiliaria.Application.Features.Properties.Commands.UpdateProperty;

namespace Inmobiliaria.Application.Features.Properties.Validators;

public class UpdatePropertyValidator : AbstractValidator<UpdatePropertyCommand>
{
    public UpdatePropertyValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id es requerido");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title es requerido")
            .MaximumLength(256).WithMessage("Title no puede superar los 256 caracteres");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description no puede superar los 1000 caracteres");

        RuleFor(x => x.Price)
            .InclusiveBetween(0, 500000)
            .WithMessage("Price debe ser <= $500,000");

        RuleFor(x => x.AreaM2)
            .InclusiveBetween(20, 10000)
            .WithMessage("AreaM2 debe estar entre 20 y 10,000 m²");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address es requerido")
            .MaximumLength(500).WithMessage("Address no puede superar los 500 caracteres");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City es requerido")
            .MaximumLength(100).WithMessage("City no puede superar los 100 caracteres");

        RuleFor(x => x.AgentId)
            .NotEmpty().WithMessage("AgentId es requerido");
    }
}

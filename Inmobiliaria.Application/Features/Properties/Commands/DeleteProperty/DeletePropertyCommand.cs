using Inmobiliaria.Application.Abstractions.Mediator;

namespace Inmobiliaria.Application.Features.Properties.Commands.DeleteProperty;

public class DeletePropertyCommand : IRequest<Guid>
{
    public required Guid Id { get; set; }
}

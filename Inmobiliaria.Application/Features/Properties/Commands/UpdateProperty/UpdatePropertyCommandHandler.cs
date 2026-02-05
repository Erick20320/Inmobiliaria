using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Utilities.Responses;
using Inmobiliaria.Domain.Exceptions;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application.Features.Properties.Commands.UpdateProperty;

public sealed class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Guid>
{
    private readonly IPropertyRepository _repository;

    public UpdatePropertyCommandHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult<Guid>> Handle(UpdatePropertyCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var property = await _repository.GetByIdAsync(command.Id);

            if (property is null)
                return ServiceResult<Guid>.Warning(command.Id, "Propiedad no encontrada.");

            property.SetTitle(command.Title);
            property.SetDescription(command.Description);
            property.SetAgentId(command.AgentId);
            property.UpdatePrice(command.Price);
            property.UpdateArea(command.AreaM2);
            property.SetAddress(command.Address);
            property.SetCity(command.City);
            property.SetIsActive(command.IsActive);

            await _repository.UpdateAsync(property);

            return ServiceResult<Guid>.Success(property.Id, "Propiedad actualizada correctamente.");
        }
        catch (DomainRuleViolationException ex)
        {
            return ServiceResult<Guid>.Fail("Error de validación al actualizar la propiedad", ex.Message);
        }
        catch (Exception ex)
        {
            return ServiceResult<Guid>.Fail("Error inesperado al actualizar la propiedad", ex.Message);
        }
    }
}

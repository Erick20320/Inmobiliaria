using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Utilities.Responses;
using Inmobiliaria.Domain.Entities;
using Inmobiliaria.Domain.Exceptions;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application.Features.Properties.Commands.CreateProperty;

public sealed class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Guid>
{
    private readonly IPropertyRepository _propertyRepository;

    public CreatePropertyCommandHandler(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<ServiceResult<Guid>> Handle(CreatePropertyCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var property = Property.Create(
                title: command.Title,
                description: command.Description,
                propertyTypeId: command.PropertyTypeId,
                agentId: command.AgentId,
                price: command.Price,
                areaM2: command.AreaM2,
                address: command.Address,
                city: command.City
            );

            var propertyId = await _propertyRepository.CreateAsync(property);

            return ServiceResult<Guid>.Success(propertyId, "Propiedad creada exitosamente");
        }
        catch (DomainRuleViolationException ex)
        {
            return ServiceResult<Guid>.Fail("Violación de regla de negocio", ex.Message);
        }
        catch (Exception ex)
        {
            return ServiceResult<Guid>.Fail("Error al crear la propiedad", ex.Message);
        }
    }
}

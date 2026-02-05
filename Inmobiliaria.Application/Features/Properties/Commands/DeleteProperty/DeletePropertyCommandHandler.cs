using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Utilities.Responses;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application.Features.Properties.Commands.DeleteProperty;

public sealed class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, Guid>
{
    private readonly IPropertyRepository _repository;

    public DeletePropertyCommandHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult<Guid>> Handle(DeletePropertyCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var property = await _repository.GetByIdAsync(command.Id);

            if (property is null)
                return ServiceResult<Guid>.Warning(command.Id, "Propiedad no encontrada.");

            await _repository.DeleteAsync(property.Id);

            return ServiceResult<Guid>.Success(property.Id, "Propiedad eliminada correctamente.");
        }
        catch (Exception ex)
        {
            return ServiceResult<Guid>.Fail("Error al eliminar la propiedad", ex.Message);
        }
    }
}

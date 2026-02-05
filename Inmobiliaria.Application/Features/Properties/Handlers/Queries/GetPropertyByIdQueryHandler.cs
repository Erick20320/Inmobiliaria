using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Features.Properties.DTOs;
using Inmobiliaria.Application.Features.Properties.Mappers;
using Inmobiliaria.Application.Features.Properties.Queries;
using Inmobiliaria.Application.Utilities.Responses;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application.Features.Properties.Handlers.Queries;

public sealed class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, GetPropertyByIdQueryDto>
{
    private readonly IPropertyRepository _repository;

    public GetPropertyByIdQueryHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult<GetPropertyByIdQueryDto>> Handle(GetPropertyByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var property = await _repository.GetByIdAsync(request.Id);

        if (property is null)
            return ServiceResult<GetPropertyByIdQueryDto>.Warning(null, "Propiedad no encontrada.");

        var dto = property.ToGetPropertyByIdDto();

        return ServiceResult<GetPropertyByIdQueryDto>.Success(dto, "Propiedad obtenida correctamente.");
    }
}

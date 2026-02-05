using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Features.Properties.DTOs;
using Inmobiliaria.Application.Features.Properties.Mappers;
using Inmobiliaria.Application.Features.Properties.Queries;
using Inmobiliaria.Application.Utilities.Responses;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application.Features.Properties.Handlers.Queries;

public sealed class GetPropertyAllQueryHandler
    : IRequestHandler<GetPropertyAllQuery, List<GetPropertyAllQueryDto>>
{
    private readonly IPropertyRepository _repository;

    public GetPropertyAllQueryHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult<List<GetPropertyAllQueryDto>>> Handle(GetPropertyAllQuery request,
        CancellationToken cancellationToken)
    {
        var properties = (await _repository.GetAllAsync()).ToList();

        var result = properties
            .Select(p => p.ToGetPropertyAllDto())
            .ToList();

        return ServiceResult<List<GetPropertyAllQueryDto>>.Success(result, "Propiedades obtenidas correctamente.");
    }
}

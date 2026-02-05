using Inmobiliaria.Application.Abstractions.Mediator;
using Inmobiliaria.Application.Features.Properties.DTOs;

namespace Inmobiliaria.Application.Features.Properties.Queries;

public class GetPropertyByIdQuery : IRequest<GetPropertyByIdQueryDto>
{
    public Guid Id { get; set; }
}

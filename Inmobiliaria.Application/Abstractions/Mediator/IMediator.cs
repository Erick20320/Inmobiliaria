using Inmobiliaria.Application.Utilities.Responses;

namespace Inmobiliaria.Application.Abstractions.Mediator;

public interface IMediator
{
    Task<ServiceResult<TResponse>> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    Task<ServiceResult> Send(IRequest request, CancellationToken cancellationToken = default);
}

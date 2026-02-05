using Inmobiliaria.Application.Utilities.Responses;

namespace Inmobiliaria.Application.Abstractions.Mediator;

public interface IRequestHandler
{
    public interface IRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<ServiceResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public interface IRequestHandler<in TRequest>
        where TRequest : IRequest
    {
        Task<ServiceResult> Handle(TRequest request, CancellationToken cancellationToken);
    }
}

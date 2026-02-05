using FluentValidation;
using Inmobiliaria.Application.Abstractions.Mediator;
using Inmobiliaria.Application.Utilities.Responses;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application.Utilities.Mediator;

public sealed class MediatorSimple : IMediator
{
    private readonly IServiceProvider _provider;

    public MediatorSimple(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<ServiceResult<TResponse>> Send<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        var validation = await Validate(request, cancellationToken);
        if (!validation.Status)
            return ServiceResult<TResponse>.FailWithValidationErrors(
                validation.Error!.Details!
            );

        var handlerType = typeof(IRequestHandler<,>)
            .MakeGenericType(request.GetType(), typeof(TResponse));

        var handler = _provider.GetService(handlerType)
            ?? throw new InvalidOperationException(
                $"Handler no encontrado para {request.GetType().Name}");

        var method = handlerType.GetMethod("Handle")!;
        return await (Task<ServiceResult<TResponse>>)
            method.Invoke(handler, [request, cancellationToken])!;
    }

    public async Task<ServiceResult> Send(
        IRequest request,
        CancellationToken cancellationToken = default)
    {
        var validation = await Validate(request, cancellationToken);
        if (!validation.Status)
            return ServiceResult.FailWithValidationErrors(
                validation.Error!.Details!
            );

        var handlerType = typeof(IRequestHandler<>)
            .MakeGenericType(request.GetType());

        var handler = _provider.GetService(handlerType)
            ?? throw new InvalidOperationException(
                $"Handler no encontrado para {request.GetType().Name}");

        var method = handlerType.GetMethod("Handle")!;
        return await (Task<ServiceResult>)
            method.Invoke(handler, [request, cancellationToken])!;
    }

    private async Task<ServiceResult> Validate(
        object request,
        CancellationToken cancellationToken)
    {
        var validatorType = typeof(IValidator<>)
            .MakeGenericType(request.GetType());

        var validator = _provider.GetService(validatorType);
        if (validator is null)
            return ServiceResult.Success();

        var validateMethod = validatorType.GetMethod(
            "ValidateAsync",
            [request.GetType(), typeof(CancellationToken)]
        )!;

        var task = (Task)validateMethod.Invoke(
            validator,
            [request, cancellationToken])!;

        await task;

        var result = (FluentValidation.Results.ValidationResult)
            task.GetType().GetProperty("Result")!.GetValue(task)!;

        if (result.IsValid)
            return ServiceResult.Success();

        var errors = result.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );

        return ServiceResult.FailWithValidationErrors(errors);
    }
}

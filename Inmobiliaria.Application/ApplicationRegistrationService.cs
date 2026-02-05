using FluentValidation;
using Inmobiliaria.Application.Abstractions.Mediator;
using Inmobiliaria.Application.Features.Authentication.Validators;
using Inmobiliaria.Application.Utilities.Mediator;
using Microsoft.Extensions.DependencyInjection;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application;

public static class ApplicationRegistrationService
{
    public static IServiceCollection AddServiceApplication(this IServiceCollection services)
    {
        services.AddTransient<IMediator, MediatorSimple>();

        services.Scan(scan =>
            scan.FromAssembliesOf(typeof(IMediator))
                .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.AddValidatorsFromAssemblies(new[] { typeof(LoginUserValidator).Assembly });

        return services;
    }
}

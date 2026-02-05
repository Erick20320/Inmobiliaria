using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Abstractions.Security;
using Inmobiliaria.Application.Abstractions.Services;
using Inmobiliaria.Persistence.Database;
using Inmobiliaria.Persistence.Repositories;
using Inmobiliaria.Persistence.Security;
using Inmobiliaria.Persistence.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inmobiliaria.Persistence;

public static class PersistenceRegistrationService
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDbConnectionFactory>(provider =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' no configurada");

            return new SqlConnectionFactory(connectionString);
        });

        // Repositorios
        services.AddScoped<IUserAuthRepository, UserAuthRepository>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IAgentRepository, AgentRepository>();

        // Servicios
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, PasswordHasherService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPropertySearchService, PropertySearchService>();

        return services;
    }
}

using Inmobiliaria.Application.DTOs.Authentication;

namespace Inmobiliaria.Application.Abstractions.Repositories;

public interface IUserAuthRepository
{
    Task<UserAuthDto?> GetByCredentialAsync(string credential);
    Task<IReadOnlyCollection<string>> GetRolesAsync(Guid userId);
}

using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Abstractions.Services;
using Inmobiliaria.Application.DTOs.Authentication;
using Inmobiliaria.Persistence.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Inmobiliaria.Persistence.Repositories;

public sealed class UserAuthRepository : BaseRepository, IUserAuthRepository
{
    public UserAuthRepository(IDbConnectionFactory factory) : base(factory)
    {
    }

    public async Task<UserAuthDto?> GetByCredentialAsync(string credential)
    {
        var parameters = new[]
        {
            new SqlParameter("@Credential", SqlDbType.NVarChar, 256) { Value = credential }
        };

        return await ExecuteReaderSingleAsync(
            "sp_User_GetByCredential",
            MapToUserAuthDto,
            parameters);
    }

    public async Task<IReadOnlyCollection<string>> GetRolesAsync(Guid userId)
    {
        var parameters = new[]
        {
            new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Value = userId }
        };

        return await ExecuteReaderListAsync(
            "sp_User_GetRoles",
            reader => reader.GetStringOrEmpty("Name"),
            parameters);
    }

    private static UserAuthDto MapToUserAuthDto(SqlDataReader reader)
    {
        return new UserAuthDto
        {
            Id = reader.GetGuidSafe("Id"),
            Email = reader.GetStringOrEmpty("Email"),
            PasswordHash = reader.GetStringOrEmpty("PasswordHash"),
            IsActive = reader.GetBooleanSafe("IsActive")
        };
    }
}

using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Abstractions.Services;
using Inmobiliaria.Domain.Entities;
using Inmobiliaria.Persistence.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Inmobiliaria.Persistence.Repositories;

public class AgentRepository(IDbConnectionFactory factory) : BaseRepository(factory), IAgentRepository
{
    #region CRUD

    public async Task<Guid> CreateAsync(Agent agent)
    {
        agent.SetId(Guid.NewGuid());

        return await ExecuteScalarAsync<Guid>(
            "sp_CreateAgent",
            new SqlParameter("@Id", agent.Id),
            new SqlParameter("@UserId", agent.UserId),
            new SqlParameter("@FullName", agent.FullName),
            new SqlParameter("@Email", agent.Email),
            new SqlParameter("@Phone", (object?)agent.Phone ?? DBNull.Value),
            new SqlParameter("@IsActive", agent.IsActive)
        );
    }

    public async Task<Agent?> GetByIdAsync(Guid id)
    {
        return await ExecuteReaderSingleAsync(
            "sp_GetAgentById",
            reader => MapAgent(reader),
            new SqlParameter("@Id", id)
        );
    }

    public async Task<IEnumerable<Agent>> GetAllAsync()
    {
        return await ExecuteReaderListAsync(
            "sp_GetAllAgents",
            reader => MapAgent(reader)
        );
    }

    public async Task UpdateAsync(Agent agent)
    {
        await ExecuteNonQueryAsync(
            "sp_UpdateAgent",
            new SqlParameter("@Id", agent.Id),
            new SqlParameter("@FullName", agent.FullName),
            new SqlParameter("@Email", agent.Email),
            new SqlParameter("@Phone", (object?)agent.Phone ?? DBNull.Value),
            new SqlParameter("@IsActive", agent.IsActive)
        );
    }

    public async Task DeleteAsync(Guid id)
    {
        await ExecuteNonQueryAsync(
            "sp_DeleteAgent",
            new SqlParameter("@Id", id)
        );
    }

    #endregion

    #region Helpers

    private static Agent MapAgent(SqlDataReader reader)
    {
        var userId = reader.GetGuid("UserId");
        var fullName = reader.GetStringOrEmpty("FullName");
        var email = reader.GetStringOrEmpty("Email");
        var phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString("Phone");

        var agent = Agent.Create(userId, fullName, email, phone);

        agent.SetId(reader.GetGuid("Id"));
        agent.SetCreatedAt(reader.GetDateTime("CreatedAt"));
        agent.SetUpdatedAt(reader.IsDBNull(reader.GetOrdinal("UpdatedAt")) ? null : reader.GetDateTime("UpdatedAt"));

        return agent;
    }

    #endregion
}

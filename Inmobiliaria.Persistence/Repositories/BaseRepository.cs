using Inmobiliaria.Application.Abstractions.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Inmobiliaria.Persistence.Repositories;

public abstract class BaseRepository
{
    private readonly IDbConnectionFactory _factory;

    protected BaseRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    protected async Task<T?> ExecuteReaderSingleAsync<T>(
        string storedProcedure,
        Func<SqlDataReader, T> mapper,
        params SqlParameter[] parameters)
    {
        await using var connection = (SqlConnection)_factory.CreateConnection();
        await using var command = CreateCommand(connection, storedProcedure, parameters);

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        return await reader.ReadAsync() ? mapper(reader) : default;
    }

    protected async Task<IReadOnlyCollection<T>> ExecuteReaderListAsync<T>(
        string storedProcedure,
        Func<SqlDataReader, T> mapper,
        params SqlParameter[] parameters)
    {
        await using var connection = (SqlConnection)_factory.CreateConnection();
        await using var command = CreateCommand(connection, storedProcedure, parameters);

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        var results = new List<T>();
        while (await reader.ReadAsync())
        {
            results.Add(mapper(reader));
        }

        return results.AsReadOnly();
    }

    protected async Task<int> ExecuteNonQueryAsync(
        string storedProcedure,
        params SqlParameter[] parameters)
    {
        await using var connection = (SqlConnection)_factory.CreateConnection();
        await using var command = CreateCommand(connection, storedProcedure, parameters);

        await connection.OpenAsync();
        return await command.ExecuteNonQueryAsync();
    }

    protected async Task<T?> ExecuteScalarAsync<T>(
        string storedProcedure,
        params SqlParameter[] parameters)
    {
        await using var connection = (SqlConnection)_factory.CreateConnection();
        await using var command = CreateCommand(connection, storedProcedure, parameters);

        await connection.OpenAsync();
        var result = await command.ExecuteScalarAsync();

        return result == null || result == DBNull.Value ? default : (T)result;
    }

    private static SqlCommand CreateCommand(
        SqlConnection connection,
        string storedProcedure,
        SqlParameter[]? parameters)
    {
        var command = new SqlCommand(storedProcedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        if (parameters != null && parameters.Length > 0)
        {
            command.Parameters.AddRange(parameters);
        }

        return command;
    }
}

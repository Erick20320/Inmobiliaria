using Inmobiliaria.Application.Abstractions.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Inmobiliaria.Persistence.Database;

public sealed class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException(
                "La cadena de conexión no puede estar vacía o nula",
                nameof(connectionString));
        }

        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}

using System.Data;

namespace Inmobiliaria.Application.Abstractions.Services;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

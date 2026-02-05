using Dapper;
using Inmobiliaria.Application.Abstractions.Security;
using Inmobiliaria.Application.Abstractions.Services;
using Microsoft.Data.SqlClient;

namespace Inmobiliaria.Persistence.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(
        IDbConnectionFactory connectionFactory,
        IPasswordHasher passwordHasher)
    {
        await using var connection = (SqlConnection)connectionFactory.CreateConnection();
        await connection.OpenAsync();

        var adminRoleId = await EnsureRoleAsync(connection, "Admin");
        var userRoleId = await EnsureRoleAsync(connection, "User");

        await EnsureUserAsync(connection, passwordHasher, "admin@demo.com", "admin", "Admin.123", adminRoleId);
        await EnsureUserAsync(connection, passwordHasher, "usuario@demo.com", "usuario", "User.123", userRoleId);
    }

    private static async Task<Guid> EnsureRoleAsync(SqlConnection connection, string roleName)
    {
        var roleId = await connection.QuerySingleOrDefaultAsync<Guid?>(
            "SELECT Id FROM Roles WHERE Name = @Name",
            new { Name = roleName });

        if (roleId.HasValue) return roleId.Value;

        var newRoleId = Guid.NewGuid();
        await connection.ExecuteAsync(
            "INSERT INTO Roles (Id, Name) VALUES (@Id, @Name)",
            new { Id = newRoleId, Name = roleName });

        return newRoleId;
    }

    private static async Task EnsureUserAsync(
        SqlConnection connection,
        IPasswordHasher passwordHasher,
        string email,
        string userName,
        string password,
        Guid roleId)
    {
        var existingUserId = await connection.QuerySingleOrDefaultAsync<Guid?>(
            "SELECT Id FROM Users WHERE Email = @Email",
            new { Email = email });

        if (existingUserId.HasValue) return;

        var userId = Guid.NewGuid();
        var passwordHash = passwordHasher.HashPassword(password);

        await connection.ExecuteAsync(
            "INSERT INTO Users (Id, UserName, Email, PasswordHash, IsActive) VALUES (@Id, @UserName, @Email, @PasswordHash, 1)",
            new { Id = userId, UserName = userName, Email = email, PasswordHash = passwordHash });

        await connection.ExecuteAsync(
            "INSERT INTO UserRoles (UserId, RoleId) VALUES (@UserId, @RoleId)",
            new { UserId = userId, RoleId = roleId });
    }
}

using BCrypt.Net;
using Inmobiliaria.Application.Abstractions.Security;

namespace Inmobiliaria.Persistence.Security;

public sealed class PasswordHasherService : IPasswordHasher
{
    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("La contraseña no puede estar vacía", nameof(password));
        }

        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }

    public bool Verify(string password, string hash)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(hash))
        {
            return false;
        }

        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        catch (SaltParseException)
        {
            // Hash inválido o corrupto
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

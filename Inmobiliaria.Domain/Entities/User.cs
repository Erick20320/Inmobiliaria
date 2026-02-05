namespace Inmobiliaria.Domain.Entities;

public sealed class User : BaseEntity
{
    public string UserName { get; private set; } = null!;
    public string NormalizedUserName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string NormalizedEmail { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    public string? SecurityStamp { get; private set; }
    public string? ConcurrencyStamp { get; private set; }
    public string? PhoneNumber { get; private set; }

    private User() { }

    public User(string userName, string email, string passwordHash)
    {
        UserName = userName;
        NormalizedUserName = userName.ToUpperInvariant();
        Email = email;
        NormalizedEmail = email.ToUpperInvariant();
        PasswordHash = passwordHash;
        SecurityStamp = Guid.NewGuid().ToString();
        ConcurrencyStamp = Guid.NewGuid().ToString();
    }
}

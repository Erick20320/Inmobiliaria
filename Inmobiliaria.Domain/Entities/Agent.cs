using Inmobiliaria.Domain.Exceptions;

namespace Inmobiliaria.Domain.Entities;

public sealed class Agent : BaseEntity
{
    public Guid UserId { get; private set; }
    public string FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string? Phone { get; private set; }
    public bool IsActive { get; private set; } = true;

    private Agent() { }

    public static Agent Create(Guid userId, string fullName, string email, string? phone)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new DomainRuleViolationException("El nombre completo es obligatorio.");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainRuleViolationException("El email es obligatorio.");

        return new Agent
        {
            UserId = userId,
            FullName = fullName.Trim(),
            Email = email.Trim(),
            Phone = phone,
            IsActive = true
        };
    }

    public void SetFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new DomainRuleViolationException("El nombre completo es obligatorio.");
        FullName = fullName.Trim();
        MarkUpdated();
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainRuleViolationException("El email es obligatorio.");
        Email = email.Trim();
        MarkUpdated();
    }

    public void SetPhone(string? phone)
    {
        Phone = phone;
        MarkUpdated();
    }

    public void Activate()
    {
        IsActive = true;
        MarkUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkUpdated();
    }

    public void SetId(Guid id) => Id = id;
    public void SetCreatedAt(DateTime createdAt) => CreatedAt = createdAt;
    public void SetUpdatedAt(DateTime? updatedAt) => UpdatedAt = updatedAt;

    private new void MarkUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}

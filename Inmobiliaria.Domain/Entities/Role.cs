namespace Inmobiliaria.Domain.Entities;

public sealed class Role : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string NormalizedName { get; private set; } = null!;
    public string? Description { get; private set; }

    private Role() { }

    public Role(string name, string? description = null)
    {
        Name = name;
        NormalizedName = name.ToUpperInvariant();
        Description = description;
    }
}

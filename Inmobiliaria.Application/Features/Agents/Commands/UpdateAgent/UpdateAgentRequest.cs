namespace Inmobiliaria.Application.Features.Agents.Commands.UpdateAgent;

public sealed class UpdateAgentRequest
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; } = true;

    public UpdateAgentCommand ToCommand(Guid id)
        => new UpdateAgentCommand
        {
            Id = id,
            FullName = FullName.Trim(),
            Email = Email.Trim(),
            Phone = Phone?.Trim(),
            IsActive = IsActive
        };
}
